import { Grid, Typography, Divider, Button } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";
export default function OrderRegistrations() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Commercial.list().then((res) => setList(res))
    }, [])

    const title = 'Commercial Cards';
    const columns = [
        { header: 'Title', accessor: 'title'},
        { header: 'Card', accessor: 'commercialCardTitle'},
        { header: 'Document', accessor: 'documentNumber'},
        { header: 'Issue Date', accessor: 'iDate'},
        { header: 'Validity Date', accessor: 'vDate'},
        { header: 'Status', accessor: 'orderRegistrationStatus'},
    ];



    const props = {list, title, columns, detailsAddress : "OrderRegistrations"}

    if (list.length == 0) return <Loadingcomponent />
    
    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>Order Registraion</Typography>
                <Divider sx={{mb: 2}} />
                
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`#`}>New Registration</Button>

            </Grid>
            <Grid item xs={12}>
            <SimpleTable {... props} />
            </Grid>





        </Grid>

    )


}