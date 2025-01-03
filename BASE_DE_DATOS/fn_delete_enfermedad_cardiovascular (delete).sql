CREATE OR REPLACE FUNCTION admece.fn_delete_enfermedad_cardiovascular(
	p_id_enf_cardiovascular integer)
    RETURNS TABLE(id_enf_cardiovascular integer) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000
AS $BODY$
 begin 
 return query
 update admece.tc_enfermedad_cardiovascular as t
 set estado=false
 where t.id_enf_cardiovascular=p_id_enf_cardiovascular
 returning t.id_enf_cardiovascular;
end
$BODY$;