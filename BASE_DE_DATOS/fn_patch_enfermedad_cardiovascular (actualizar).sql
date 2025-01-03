
CREATE OR REPLACE FUNCTION admece.fn_patch_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer,p_nombre varchar,
	p_descripcion varchar, p_estado boolean)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre varchar, descripcion varchar, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
 begin
 return query
 update admece.tc_enfermedad_cardiovascular as t
 set nombre=p_nombre, descripcion=p_descripcion, fecha_actualizacion=current_date
 where t.id_enf_cardiovascular=p_id_enf_cardiovascular
 returning t.id_enf_cardiovascular,t.nombre,t.descripcion,t.estado;
end;
$BODY$;