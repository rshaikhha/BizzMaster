import { Grid, Typography, Divider, Button } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";
export default function Suppliers() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Suppliers.list().then((res) => setList(res))
    }, [])

    const title = 'Suppliers';
    const columns = [
        { header: 'Title', accessor: 'title'},
        { header: 'Full title', accessor: 'fullTitle'},
        { header: 'Country', accessor: 'country'},
    ];



    const props = {list, title, columns, detailsAddress : "Suppliers"}

    if (list.length == 0) return <Loadingcomponent />
    
    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>Suppliers</Typography>
                <Divider sx={{mb: 2}} />
                
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`#`}>New Supplier</Button>

            </Grid>
            <Grid item xs={12}>
            <SimpleTable {... props} />
            </Grid>





        </Grid>

    )


}