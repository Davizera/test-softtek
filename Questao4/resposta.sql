select assunto, ano, count(assunto) as "QUANTIDADE"
    from atendimentos
    group by assunto, ano
    having count(assunto) > 3
    order by 
        ano desc,
        count(assunto) desc 