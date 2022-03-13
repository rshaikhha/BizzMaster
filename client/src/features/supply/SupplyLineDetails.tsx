import { Grid, Typography, Divider, TableContainer, Table, TableBody, TableRow, TableCell, TextField, ButtonGroup, Button, List, ListItem, Box, MenuItem } from "@mui/material";
import { useState, useEffect } from "react";
import { useForm, Control, useWatch, useFieldArray } from "react-hook-form";
import { NavLink, useHistory, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { useAppSelector } from "../../app/store/configureStore";
import SimpleTable from "../shared/SimpleTable";
import CalculateIcon from '@mui/icons-material/Calculate';
import CloudDownloadIcon from '@mui/icons-material/CloudDownload';


type FormValues = {
    supplyLineId: number;
    year: number;
    month: number;
};

export default function SupplyLineDetails() {

    const [single, setSingle] = useState<any>(null)
    const [list, setList] = useState<any[]>([])
    const { id } = useParams<{ id: string }>();

    const history = useHistory();

    const {yearsList, monthList} = useAppSelector(state => state.basics)
    
    const { register, control, handleSubmit, formState: { errors }, reset, setError } = useForm<FormValues>({
        defaultValues: {
        },
        mode: "all"
    });

    const onSubmit = (data: FormValues) => {
        data.supplyLineId = parseInt(id);
        agent.Order.post(data).then(() => history.push(`/Order/${id}`))
    };

    const onLoad = (data: FormValues) => {
        data.supplyLineId = parseInt(id);
        agent.Audit.get(data.supplyLineId, data.year, data.month).then((res) => setList(res));
    };

    useEffect(() => {
        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
    }, [])




    if (!single) return <Loadingcomponent />
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
                
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`/Stock/${id}`}>Openning Stock</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="one" component ={NavLink} to={`/SalesForecast/${id}`}>ForeCast</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="tree" component ={NavLink} to={`/Order/${id}`}>Order</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="four" component ={NavLink} to={`/SupplyLineAudit/${id}`}>Audit</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="five"component ={NavLink} to={`/SupplyLines`}>Back</Button>
            </Grid>
            <Grid item xs={12}>
            <SimpleTable {... props} />
            </Grid>





        </Grid>
    )
}