
CREATE OR REPLACE FUNCTION schemasye.fn_getcomplete_enfermedad_cronica()
    RETURNS TABLE(id_enf_cronica integer, nombre varchar, descripcion varchar, fecha_registro date, fecha_inicio date, estado boolean, fecha_actualizacion date) 
	LANGUAGE 'plpgsql'
    COST 100
    STABLE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
 begin
 return QUERY 
 select e.id_enf_cronica,e.nombre,e.descripcion,e.fecha_registro, e.fecha_inicio, e.estado, e.fecha_actualizacion
 from schemasye.tc_enfermedad_cronica e
 where e.estado=true	order by e.id_enf_cronica asc;
end;
$BODY$;