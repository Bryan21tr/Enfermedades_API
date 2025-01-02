using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivoFijoAPI.Util;
using Npgsql;
using TsaakAPI.Entities;

using ConnectionTools.DBTools;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace TsaakAPI.Model.DAO
{
    public class EnfermedadCronicaDao
    {
         private ISqlTools _sqlTools;
        
         public EnfermedadCronicaDao(string connectionString) 
        {
            this._sqlTools = new SQLTools(connectionString);
            
        }

public async Task<ResultOperation<List<VMCatalog>>> GetAll()
        {
            List<VMCatalog> Lista = new List<VMCatalog>();
            ResultOperation<List<VMCatalog>> resultOperation = new ResultOperation<List<VMCatalog>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("shemasye.fn_getall_enfermedad_cronica", new ParameterPGsql[]{});
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0
                 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow fila in respuestaBD.Data.Tables[0].Rows){
                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)fila["id_enf_cronica"],
                        Nombre = fila["nombre"].ToString(),
                        Descripcion = fila["descripcion"].ToString(),
                        Estado = fila["estado"] as bool?,

                    }; 
                    Lista.Add(aux);
                    }
                    resultOperation.Result = Lista; 
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
                
            }
            else
            {
                //TODO Agregar error en el log             
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }
            return resultOperation;
        }
      

        public async Task<ResultOperation<VMCatalog>> GetByIdAsync(int id)
        {
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("shemasye.fn_get_enfermedad_cronica", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,id),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"],
                        Nombre = respuestaBD.Data.Tables[0].Rows[0]["nombre"].ToString(),
                        Descripcion = respuestaBD.Data.Tables[0].Rows[0]["descripcion"].ToString(),
                        Estado = respuestaBD.Data.Tables[0].Rows[0]["estado"] as bool?,

                    }; 

                    resultOperation.Result = aux; 
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
                
            }
            else
            {
                //TODO Agregar error en el log             
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }
            return resultOperation;


        }
        public async Task<ResultOperation<int>> Create(EnfermedadCronica enfermedad){
            ResultOperation<int> resultOperation = new ResultOperation<int>();
            try{
                Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("shemasye.fn_insert_enfermedad_cronica", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,enfermedad.id_enf_cronica),
                    new ParameterPGsql("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.nombre),
                    new ParameterPGsql("p_descripcion", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.descripcion),
                    new ParameterPGsql("p_fecha_registro", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_registro),
                    new ParameterPGsql("p_fecha_inicio", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_inicio),
                    new ParameterPGsql("p_estado", NpgsqlTypes.NpgsqlDbType.Boolean,enfermedad.estado),
                    new ParameterPGsql("p_fecha_actualizacion", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_actualizacion)
                });
                RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                        resultOperation.Result = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"];   
                }
                else
                {
                    resultOperation.Result = 0;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
            }
            }catch (Exception ex)
            {
                // Captura errores no controlados
                resultOperation.Success = false;
                resultOperation.AddErrorMessage($"Error al insertar el registro en la base de datos: {ex.Message}");
            }
            return resultOperation;
        }
        public async Task<ResultOperation<VMCatalog>> Update(EnfermedadCronica enfermedad, int id){
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();
            try{
                Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("shemasye.fn_patch_enfermedad_cronica", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,id),
                    new ParameterPGsql("p_nombre", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.nombre),
                    new ParameterPGsql("p_descripcion", NpgsqlTypes.NpgsqlDbType.Varchar,enfermedad.descripcion),
                    new ParameterPGsql("p_fecha_registro", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_registro),
                    new ParameterPGsql("p_fecha_inicio", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_inicio),
                    new ParameterPGsql("p_estado", NpgsqlTypes.NpgsqlDbType.Boolean,enfermedad.estado),
                    new ParameterPGsql("p_fecha_actualizacion", NpgsqlTypes.NpgsqlDbType.Date,enfermedad.fecha_actualizacion)
                });
                RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {
                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"],
                        Nombre = respuestaBD.Data.Tables[0].Rows[0]["nombre"].ToString(),
                        Descripcion = respuestaBD.Data.Tables[0].Rows[0]["descripcion"].ToString(),
                        Estado = respuestaBD.Data.Tables[0].Rows[0]["estado"] as bool?,

                    }; 

                    resultOperation.Result = aux;                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabla. {respuestaBD.Detail}");
                }
            }
            }catch (Exception ex)
            {
                // Captura errores no controlados
                resultOperation.Success = false;
                resultOperation.AddErrorMessage($"Error al insertar el registro en la base de datos: {ex.Message}");
            }
            return resultOperation;
        }

        public async Task<ResultOperation<VMCatalog>> Delete(int id)
        {
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("shemasye.fn_delete_enfermedad_cronica", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cronica", NpgsqlTypes.NpgsqlDbType.Integer,id),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cronica"]

                    }; 

                    resultOperation.Result = aux; 
                }
                else
                {
                    resultOperation.Result = null;
                    resultOperation.Success = false;
                    resultOperation.AddErrorMessage($"No fue posible regresar el registro de la tabl. {respuestaBD.Detail}");
                }
                
            }
            else
            {
                //TODO Agregar error en el log             
                if (respuestaBD.ExisteError)
                    Console.WriteLine("Error {0} - {1} - {2} - {3}", respuestaBD.ExisteError, respuestaBD.Mensaje, respuestaBD.CodeSqlError, respuestaBD.Detail);
                throw new Exception(respuestaBD.Mensaje);
            }
            return resultOperation;


        }
               
    }
}

