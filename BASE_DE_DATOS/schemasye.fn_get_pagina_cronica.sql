CREATE OR REPLACE FUNCTION schemasye.fn_get_pagina_cronica(
	p_id_pag_cronica integer,p_id_fecth_cronica integer)
    RETURNS TABLE(id_enf_cronica integer, nombre character varying, descripcion character varying, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
begin
 return QUERY
 execute format(' select e.id_enf_cronica,e.nombre,e.descripcion,e.estado
 from schemasye.tc_enfermedad_cronica e
 order by id_enf_cronica limit %s offset %s', p_id_fecth_cronica,(p_id_pag_cronica-1)*p_id_fecth_cronica);
end;
$BODY$;