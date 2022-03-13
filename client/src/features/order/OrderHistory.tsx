import { Grid, Typography, Divider, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Product } from "../../app/models/product";

export default function OrderHistory() {
    const [single, setSingle] = useState<any>(null)
    const [list, setList] = useState<any>(null)
    const [products, setProducts] = useState<Product[]>([])
    const { id, year, month } = useParams<{id: string; year: string; month: string}>();

    useEffect(() => {

        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
        agent.Order.history(parseInt(id),parseInt(year), parseInt(month)).then((res) => setList(res))
        //agent.Suppliers.activeProducts(parseInt(id)).then((res) => setProducts(res))
    }, [])




    if (!single || !list || !products) return <Loadingcomponent />
console.log(single)

    const columns = [

        { header: 'Year', accessor: 'year' },
        { header: 'Month', accessor: 'month' },
        { header: 'Total Quantity', accessor: 'totalQuantity' },

    ];
    const props = { list, title: "Sales Forecast", columns, detailsAddress: "SalesForecastDetails" }
    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>{single.title}</Typography>
                <Typography variant='body2'>Supplier: {single.supplier}</Typography>
                <Divider sx={{ mb: 2 }} />

                <Button variant="contained" sx={{ m: 1, minWidth: '200px' }} key="one" component={NavLink} to={`/SalesForecast/${id}`}>Back</Button>
            </Grid>
            <Grid item xs={12}>
                <Paper sx={{ padding: 2 }}>

                    <Typography variant='h4'>Order History </Typography>

                    <TableContainer >
                        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                            <TableHead
                                sx={{ bgcolor: 'grey.300' }}
                            >
                                <TableRow>
                                    <TableCell key="index">Index</TableCell>
                                    <TableCell key="PartNumner">Part Number</TableCell>
                                    {list.map((item: any, index: number) => <TableCell key={"header" + index}>
                                        {item.year} / {item.month}
                                        </TableCell>)}

                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {single.products.map((row: Product, rowindex: number) => (
                                    <TableRow
                                        key={row.id}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 }, '&:hover': { backgroundColor: 'grey.200' }, textDecoration: 'none' }}
                                    >
                                        <TableCell key={row.id + "-index-" + {rowindex}}>{rowindex + 1}</TableCell>
                                        <TableCell key={row.id + "-PN-" + {rowindex}}>{row.partNumber}</TableCell>
                                        {list.map((item: any, colindex: number) => {
                                            const itemlist = item.items;
                                            const quantity = itemlist.find((x: any) => (x.productId == row.id))?.quantity || 0;
                                            return (

                                                <TableCell key={rowindex + "-" + colindex + item.year + item.month}>{quantity}</TableCell>)
                                        }
                                        )

                                        }
                                        {/* <TableCell key='link'><Button component={NavLink} to={`/${props.detailsAddress}/${row.id}`}></Button></TableCell> */}
                                    </TableRow>
                                ))}
                                <TableRow sx={{backgroundColor:"grey.200"}}>
                                    <TableCell key="sumindex"></TableCell>
                                    <TableCell key="sumPartNumner"></TableCell>
                                    {list.map((item: any, index: number) => <TableCell key={index}>{item.totalQuantity}</TableCell>)}

                                </TableRow>
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            </Grid>
        </Grid>
    )
}