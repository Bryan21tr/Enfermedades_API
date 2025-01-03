
CREATE OR REPLACE FUNCTION schemasye.fn_patch_enfermedad_cronica(
	p_id_enf_cronica integer,
	p_nombre varchar,p_descripcion varchar,p_estado boolean)
    RETURNS TABLE(id_enf_cronica integer, nombre varchar, descripcion varchar, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
 begin
 return query
 update schemasye.tc_enfermedad_cronica as t
 set nombre=p_nombre,descripcion=p_descripcion,fecha_actualizacion=current_date
 where t.id_enf_cronica=p_id_enf_cronica
  returning t.id_enf_cronica,t.nombre,t.descripcion,t.estado;
end
$BODY$;