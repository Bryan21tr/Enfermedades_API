CREATE OR REPLACE FUNCTION admece.fn_get_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
 begin
 return QUERY 
 select e.id_enf_cardiovascular,e.nombre,e.descripcion,e.fecha_registro, e.fecha_inicio, e.estado, e.fecha_actualizacion
 from admece."tc_enfermedad_cardiovascular" e
 where e.id_enf_cardiovascular = p_id_enf_cardiovascular;
end
$BODY$;


