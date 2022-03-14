import { Grid, Typography, Divider, Button } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";
export default function Projects() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Projects.list().then((res) => setList(res))
    }, [])

    const title = 'Projects';
    const columns = [
        { header: 'Title', accessor: 'title'},
        { header: 'Supply Line', accessor: 'supplyLine'},
        // { header: 'Order', accessor: 'orderTitle'},
        { header: 'Planned Start', accessor: 'pStart'},
        { header: 'Planned Finish', accessor: 'pFinish'},
        { header: 'Estimated Start', accessor: 'eStart'},
        { header: 'Estimated Finish', accessor: 'eFinish'},
        { header: 'Actual Start', accessor: 'aStart'},
        { header: 'Actual Finish', accessor: 'aFinish'},
        { header: 'Status', accessor: 'status'},
        { header: 'CompleteRate', accessor: 'completeRate'},
        
    ];



    const props = {list, title, columns, detailsAddress : "ProjectDetail"}

    if (!list) return <Loadingcomponent />
    
    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>Projects</Typography>
                <Divider sx={{mb: 2}} />
                
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`/ProjectWizard1`}>New Project</Button>

            </Grid>
            <Grid item xs={12}>
            <SimpleTable {... props} />
            </Grid>





        </Grid>

    )


}