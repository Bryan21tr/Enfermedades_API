
CREATE OR REPLACE FUNCTION schemasye.fn_insert_enfermedad_cronica(
	p_id_enf_cronica integer,p_nombre varchar,
	p_descripcion varchar,p_fecha_registro date,
	p_fecha_inicio date,p_estado boolean,
	p_fecha_actualizacion date)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
 begin
 return query
 insert into schemasye.tc_enfermedad_cronica as t
 (id_enf_cronica,nombre,descripcion,fecha_registro,fecha_inicio , estado, fecha_actualizacion)
values(p_id_enf_cronica,p_nombre,p_descripcion,p_fecha_registro,p_fecha_inicio , p_estado, p_fecha_actualizacion)
returning t.id_enf_cronica,t.nombre,t.descripcion,t.fecha_registro,t.fecha_inicio,t.estado,t.fecha_actualizacion;
end
$BODY$;