import { Grid, Typography, Divider, TableContainer, Table, TableBody, TableRow, TableCell, TextField, ButtonGroup, Button, List, ListItem } from "@mui/material";
import { useState, useEffect } from "react";
import { NavLink, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";

export default function SupplyLineDetails() {

    const [single, setSingle] = useState<any>(null)
    const { id } = useParams<{ id: string }>();



    useEffect(() => {
        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
    }, [])




    if (!single) return <Loadingcomponent message='Loading Products ...' />
    const columns = [
         
        { header: 'Part Number', accessor: 'partNumber'},
        { header: 'Description', accessor: 'description'},
        { header: 'Category', accessor: 'category'},
        { header: 'Brand', accessor: 'brand'},

    ];
    const props = {list : single.products , title : "Products", columns, detailsAddress : "Products"}
    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>{single.title}</Typography>
                <Typography variant='body2'>Supplier: {single.supplier}</Typography>
                <Divider sx={{mb: 2}} />
                
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="one" component ={NavLink} to={`/SalesForecast/${id}`}>ForeCast</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`/SalesForecast/${id}`}>ForeCast</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="tree">tree</Button>
            </Grid>
            <Grid item xs={12}>
            <SimpleTable {... props} />
            </Grid>
        </Grid>
    )
}