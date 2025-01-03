
CREATE OR REPLACE FUNCTION admece.fn_get_pagina_cardiovascular(
	p_id_pag_cardiovascular integer,p_id_fecth_cardiovascular integer)
    RETURNS TABLE(id_enf_cardiovascular integer, nombre character varying, descripcion character varying, estado boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
begin
 return QUERY
 execute format(' select e.id_enf_cardiovascular,e.nombre,e.descripcion,e.estado
 from admece.tc_enfermedad_cardiovascular e
 order by id_enf_cardiovascular limit %s offset %s', p_id_fecth_cardiovascular,(p_id_pag_cardiovascular-1)*p_id_fecth_cardiovascular);
end;
$BODY$;