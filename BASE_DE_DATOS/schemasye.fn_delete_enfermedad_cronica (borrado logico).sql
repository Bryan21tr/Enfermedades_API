
CREATE OR REPLACE FUNCTION schemasye.fn_delete_enfermedad_cronica(p_id_enf_cronica integer)
    RETURNS TABLE(id_enf_cronica integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
 begin 
 return query
 update schemasye.tc_enfermedad_cronica as t
 set estado=false
 where t.id_enf_cronica=p_id_enf_cronica
 returning t.id_enf_cronica;
end;
$BODY$;