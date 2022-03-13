import { Grid, Typography, Divider, TableContainer, Table, TableBody, TableRow, TableCell, TextField, ButtonGroup, Button, List, ListItem, Box, MenuItem, Paper, TableHead } from "@mui/material";
import { useState, useEffect } from "react";
import { useForm, Control, useWatch, useFieldArray } from "react-hook-form";
import { NavLink, useHistory, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { useAppSelector } from "../../app/store/configureStore";
import SimpleTable from "../shared/SimpleTable";
import CalculateIcon from '@mui/icons-material/Calculate';
import CloudDownloadIcon from '@mui/icons-material/CloudDownload';
import { title } from "process";


type FormValues = {
    supplyLineId: number;
    year: number;
    month: number;
};

export default function SupplyLineDetails() {

    const [single, setSingle] = useState<any>(null)
    const [audit, setAudit] = useState<any>()
    const { id } = useParams<{ id: string }>();

    const history = useHistory();

    const {yearsList, monthList} = useAppSelector(state => state.basics)
    const columns = [
         
        { header: 'Part Number', accessor: 'productPartNumber'},
        { header: 'Openning Stock', accessor: 'openningStock'},
        { header: 'Sales Forecast', accessor: 'sales'},
        { header: 'Order', accessor: 'order'},
        { header: 'Shortage', accessor: 'shortage'},

    ];
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
        agent.Audit.get(data.supplyLineId, data.year, data.month).then((res) =>{
             setAudit(res)
             console.log(res)
            });

        
    };

    useEffect(() => {
        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
    }, [])




    if (!single ) return <Loadingcomponent />
    
    
    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>{single.title}</Typography>
                <Typography variant='body2'>Supplier: {single.supplier}</Typography>
                <Divider sx={{mb: 2}} />
                
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="two" component ={NavLink} to={`/Stock/${id}`}>Openning Stock</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="one" component ={NavLink} to={`/SalesForecast/${id}`}>ForeCast</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="tree" component ={NavLink} to={`/Order/${id}`}>Order</Button>
                <Button variant="contained" sx={{m : 1 , minWidth : '200px'}} key="four"component ={NavLink} to={`/SupplyLines`}>Back</Button>
            </Grid>

            <Grid item xs={12}>

            
            <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate sx={{ mt: 1 }}>
                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }}>
                    <Grid item xs={3}>
                        <TextField
                            id="year"
                            select
                            label="Select Year"
                            {...register('year', { required: 'year is required' })}
                            error={!!errors.year}
                            helperText={errors?.year?.message}
                            placeholder="Year"
                            defaultValue={1401}
                            // sx={{ m: 1, minWidth: '50%' }}
                            fullWidth
                        >
                            {yearsList.map((option) => (
                                <MenuItem key={option} value={option}>
                                    {option}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item xs={3}>
                        <TextField
                            id="month"
                            select
                            label="Select Month"
                            {...register('month', { required: 'month is required' })}
                            error={!!errors.month}
                            helperText={errors?.month?.message}
                            placeholder="Month"
                            // sx={{ m: 1, minWidth: '50%' }}
                            defaultValue={1}
                            fullWidth
                        >
                            {monthList.map((option, index) => (
                                <MenuItem key={option} value={index + 1}>
                                    {option}
                                </MenuItem>
                            ))}

                        </TextField>

                    </Grid>
                    <Grid item xs={3}>

                        <Button variant="outlined" color="primary" onClick={handleSubmit(onLoad)}>
                            <CloudDownloadIcon />
                            <Typography variant="body2" sx={{ m: 1 }}>Load Data</Typography>

                        </Button>
                    </Grid>


                </Grid>

            </Box>
            </Grid>

            <Grid item xs={12}>
                {audit && 
                        <Paper sx={{ padding: 2 }}>

                        <Typography variant='h4'>Audit Report</Typography>
                
                        <TableContainer >
                            <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                                <TableHead
                                sx ={{bgcolor: 'grey.300'}}
                                >
                                    <TableRow>
                                        <TableCell key="index">Index</TableCell>
                                        {columns.map((item) => <TableCell key={item.header}>{item.header}</TableCell>)}
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {audit.items.map((row: any, index: number) => (
                                        <TableRow
                                            key={index}
                                            sx={{ '&:last-child td, &:last-child th': { border: 0 }, '&:hover': { backgroundColor: 'grey.200' }, textDecoration: 'none' } }
                                        >
                                            <TableCell>{1 + index}</TableCell>
                                            {columns.map((item, itemIndex) => (
                                                <TableCell key={itemIndex}>{row[item.accessor]}</TableCell>
                                            ))}
                                        </TableRow>
                                    ))}
                                    <TableRow sx={{backgroundColor:"grey.200"}}>
                                    <TableCell key="sumindex"></TableCell>
                                    <TableCell key="sumPartNumber"></TableCell>
                                    <TableCell key="totalOpenningStock">{audit.totalOpenningStock}</TableCell>
                                    <TableCell key="TotalSales">{audit.totalSales}</TableCell>
                                    <TableCell key="TotalOrder">{audit.totalOrder}</TableCell>
                                    <TableCell key="TotalShortage">{audit.totalShortage}</TableCell>

                                </TableRow>
                                </TableBody>
                            </Table>
                        </TableContainer>
                        </Paper>
                
                }
            
            </Grid>



        </Grid>
    )
}