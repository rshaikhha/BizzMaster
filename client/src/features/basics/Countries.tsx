import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function Countries() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.countries.list().then((res) => setList(res))
    }, [])

    const title = 'Countries';
    const columns = [
        { header: 'Name', accessor: 'title'},
        { header: 'Abbreviation', accessor: 'abbr'}

    ];

    const props = {list, title, columns}

    return (
        <SimpleTable {...props} />

    )


}