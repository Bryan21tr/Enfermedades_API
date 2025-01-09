
CREATE OR REPLACE FUNCTION schemasye.fn_getall_enfermedad_cronica(
	)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
begin
 return QUERY
 select e.id_enf_cronica,e.nombre,e.descripcion,e.estado
 from schemasye.tc_enfermedad_cronica e
where e.estado=true;
end
$BODY$;