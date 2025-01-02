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
    public class EnfermedadCardiovascularDao
    {
         private ISqlTools _sqlTools;
        
        

         public EnfermedadCardiovascularDao(string connectionString) 
        {
            this._sqlTools = new SQLTools(connectionString);
            
        }

public async Task<ResultOperation<List<VMCatalog>>> GetAll()
        {
            List<VMCatalog> Lista = new List<VMCatalog>();
            ResultOperation<List<VMCatalog>> resultOperation = new ResultOperation<List<VMCatalog>>();

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_getall_enfermedad_cardiovascular", new ParameterPGsql[]{});
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
                        Id = (int)fila["id_enf_cardiovascular"],
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

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_get_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,id),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"],
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
        public async Task<ResultOperation<int>> Create(EnfermedadCardiovascular enfermedad){
            ResultOperation<int> resultOperation = new ResultOperation<int>();
            try{
                Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_insert_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,enfermedad.id_enf_cardiovascular),
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
                        resultOperation.Result = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"];   
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
        public async Task<ResultOperation<VMCatalog>> Update(EnfermedadCardiovascular enfermedad, int id){
            ResultOperation<VMCatalog> resultOperation = new ResultOperation<VMCatalog>();
            try{
                Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_patch_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,id),
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
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"],
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

            Task<RespuestaBD> respuestaBDTask = _sqlTools.ExecuteFunctionAsync("admece.fn_delete_enfermedad_cardiovascular", new ParameterPGsql[]{
                    new ParameterPGsql("p_id_enf_cardiovascular", NpgsqlTypes.NpgsqlDbType.Integer,id),
                });
            RespuestaBD respuestaBD = await respuestaBDTask;
            resultOperation.Success = !respuestaBD.ExisteError;
            if (!respuestaBD.ExisteError)
            {
                if (respuestaBD.Data.Tables.Count > 0 && respuestaBD.Data.Tables[0].Rows.Count > 0)
                {

                    VMCatalog aux = new VMCatalog
                    {
                        Id = (int)respuestaBD.Data.Tables[0].Rows[0]["id_enf_cardiovascular"]

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




  /*public async Task<ResultOperation<List<EnfermedadCardiovascular>>> ObtenerEnfermedadCardiovascular()
        {
            _logger.LogInformation("Inicio de la operación DerechohabienciaDAO");

            try
            {
                _logger.LogInformation("Respuesta exitosa de ObtenerEnfermedadCardiovascular");
                var result = new ResultOperation<List<EnfermedadCardiovascular>>();
                var derechohabList = new List<EnfermedadCardiovascular>();

                try
                {
                    await _connection.OpenAsync();

                    var sqlCommand = "select id_enf_cardiovascular, nombre, descripcion, fecha_registro, fecha_inicio, estado, fecha_actualizacion from admece.tc_enfermedad_cardiovascular";

                    using var cmd = new NpgsqlCommand(sqlCommand, _connection);

                    using var reader = await cmd.ExecuteReaderAsync();

                    // Loop through all records returned by the reader
                    while (await reader.ReadAsync())
                    {
                        var enfcardiovascular = new EnfermedadCardiovascular
                        {
                            id_enf_cardiovascular = reader["id_enf_cardiovascular"] != DBNull.Value 
                                ? Convert.ToInt32(reader["id_enf_cardiovascular"]) 
                                : 0,

                            nombre = reader["nombre"]?.ToString(),

                            descripcion = reader["descripcion"]?.ToString(),

                            fecha_registro = reader["fecha_inicio"] != DBNull.Value
                                ? DateOnly.FromDateTime(Convert.ToDateTime(reader["fecha_registro"]))
                                : default,

                            fecha_inicio = reader["fecha_inicio"] != DBNull.Value
                                ? DateOnly.FromDateTime(Convert.ToDateTime(reader["fecha_inicio"]))
                                : default,

                            estado = reader["estado"] != DBNull.Value 
                                ? Convert.ToBoolean(reader["estado"]) 
                                : false,

                            fecha_actualizacion = reader["fecha_actualizacion"] != DBNull.Value
                                ? DateOnly.FromDateTime(Convert.ToDateTime(reader["fecha_inicio"]))
                                : default
                        };

                        // Add each Derechohabiencia to the list
                        derechohabList.Add(enfcardiovascular);
                    }

                    if (derechohabList.Count > 0)
                    {
                        result.Result = derechohabList;
                        result.Success = true;
                        result.AddSuccessMessage("");
                    }
                    else
                    {
                        result.Result = new List<EnfermedadCardiovascular>();
                        result.Success = false;
                        result.AddInformationMessage("No se encontraron registros de derechohabiencia.");
                    }
                }
                catch (Exception ex)
                {
                    result.Result = new List<EnfermedadCardiovascular>();
                    result.Success = false;
                    result.AddErrorMessage($"Error al verificar la existencia de la actualización en la base de datos: {ex.Message}");
                }
                finally
                {
                    await _connection.CloseAsync();
                }

                return result;
            }
            catch (System.Exception)
            {
                _logger.LogError("Respuesta no exitosa de ObtenerEnfermedadCardiovascular");
                throw new Exception("Error interno del servidor");
            }

        }*/
