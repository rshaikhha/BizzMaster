import { Grid, Typography, Divider, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Product } from "../../app/models/product";

export default function LeadTimeHistory() {
    const [single, setSingle] = useState<any>(null)
    const [list, setList] = useState<any>(null)
    const [products, setProducts] = useState<Product[]>([])
    const { id, year, month } = useParams<{id: string; year: string; month: string}>();

    useEffect(() => {

        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
        agent.Suppliers.LeadTimeHistory(parseInt(id)).then((res) => setList(res))
        //agent.Suppliers.activeProducts(parseInt(id)).then((res) => setProducts(res))
    }, [])




    if (!single || !list || !products) return <Loadingcomponent />


    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>{single.title}</Typography>
                <Typography variant='body2'>Supplier: {single.supplier}</Typography>
                <Divider sx={{ mb: 2 }} />

                <Button variant="contained" sx={{ m: 1, minWidth: '200px' }} key="one" component={NavLink} to={`/LeadTime/${id}`}>Back</Button>
            </Grid>
            <Grid item xs={12}>
                    {list.map((version: any, rowindex: number) => (
                <Paper sx={{ padding: 2 }}>

                    <Typography variant='h4'>{(new Date(version.createdOn)).toLocaleDateString("en-GB", {
  day: "numeric",
  month: "long",
  year: "numeric",
  hour: "numeric",
  minute: "numeric"
})}</Typography>
                    <TableContainer >
                        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                            <TableHead sx={{ bgcolor: 'grey.300' }}>
                                <TableRow>
                                    <TableCell key="index">Index</TableCell>
                                    <TableCell key="title">Title</TableCell>
                                    <TableCell key="duration">Duration</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {version.items.map((item: any, index: number) => (
                                    <TableRow
                                        key={version.title}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 }, '&:hover': { backgroundColor: 'grey.200' }, textDecoration: 'none' }}
                                    >
                                        <TableCell key={"index-" + {rowindex}}>{index + 1}</TableCell>
                                        <TableCell key={"Title-" + {rowindex}}>{item.title}</TableCell>
                                        <TableCell key={"Dur-" + {rowindex}}>{item.duration}</TableCell>

                                    </TableRow>
                                ))}
                                <TableRow sx={{backgroundColor:"grey.200"}}>
                                    <TableCell key="sumindex"></TableCell>
                                    <TableCell key="sumPartNumner"></TableCell>
                                    <TableCell key="sumdur">{version.totalDuration}</TableCell>

                                </TableRow>
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
                    ))}
            </Grid>
        </Grid>
    )
}