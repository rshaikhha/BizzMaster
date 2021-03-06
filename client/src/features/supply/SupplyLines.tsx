import { Grid, Typography, Divider, Button } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";
export default function SupplyLines() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Suppliers.lines().then((res) => setList(res))
    }, [])

    const title = 'Supply Lines';
    const columns = [
        { header: 'Title', accessor: 'title'},
        { header: 'Supplier', accessor: 'supplier'},
        { header: 'Planning Type', accessor: 'defaultPlanningType'},
    ];



    const props = {list, title, columns, detailsAddress : "SupplyLineDetails"}

    if (list.length == 0) return <Loadingcomponent />
    
    return (
        <Grid container spacing={6}>
        <Grid item xs={12}>
            <Typography variant='h4'>Supply Lines</Typography>
            <Divider sx={{mb: 2}} />
            
            <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`#`}>New Line</Button>

        </Grid>
        <Grid item xs={12}>
        <SimpleTable {... props} />
        </Grid>





    </Grid>
    )


}