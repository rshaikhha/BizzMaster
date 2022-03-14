import { ExpandMore } from "@mui/icons-material";
import { Grid, Typography, Divider, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import { NavLink, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Product } from "../../app/models/product";

export default function LeadTime() {
    const [single, setSingle] = useState<any>(null)
    const [leadtime, setLeadtime] = useState<any>(null)

    const { id } = useParams<{ id: string }>();

    useEffect(() => {

        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
        agent.Suppliers.LeadTimes(parseInt(id)).then((res) => setLeadtime(res))

    }, [])




    if (!single || !leadtime) return <Loadingcomponent />


    return (
        <Grid container spacing={6}>
            <Grid item xs={12}>
                <Typography variant='h4'>{single.title}</Typography>
                <Typography variant='body2'>Supplier: {single.supplier}</Typography>
                <Divider sx={{ mb: 2 }} />

                <Button variant="contained" sx={{ m: 1, minWidth: '200px' }} key="one" component={NavLink} to={`/SubmitLeadTime/${id}`}>New LeadTime</Button>
                <Button variant="contained" sx={{ m: 1, minWidth: '200px' }} key="two" component={NavLink} to={`/SupplyLineDetails/${id}`}>Back</Button>

            </Grid>
            <Grid item xs={12}>
                <Paper sx={{ padding: 2 }}>

                    <Typography variant='h4'>Lead Time</Typography>

                    <TableContainer >
                        <Table sx={{ minWidth: 650 }} size="small" aria-label="a dense table">
                            <TableHead
                                sx={{ bgcolor: 'grey.300' }}
                            >
                                <TableRow>
                                    <TableCell key="index">Index</TableCell>
                                    <TableCell key="Activity">Activity</TableCell>
                                    <TableCell key="Duration">Duration
                                        <Button component={NavLink} to={`/LeadTimeHistory/${id}`} ><ExpandMore  /> </Button>
                                        </TableCell>

                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {leadtime.items.map((row: any, index: number) => (
                                    <TableRow
                                        key={row.id}
                                        sx={{ '&:last-child td, &:last-child th': { border: 0 }, '&:hover': { backgroundColor: 'grey.200' }, textDecoration: 'none' }}
                                    >
                                        <TableCell key={row.id + "-index"}>{index + 1}</TableCell>
                                        <TableCell key={row.id + "-title"}>{row.title}</TableCell>
                                        <TableCell key={row.id + "-duration"}>{row.duration}</TableCell>
                                    </TableRow>
                                ))}
                                <TableRow sx={{backgroundColor:"grey.200"}}>
                                    <TableCell key="sumindex"></TableCell>
                                    <TableCell key="sumPartNumner"></TableCell>
                                    <TableCell key="sum">{leadtime.totalDuration}</TableCell>

                                </TableRow>
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            </Grid>
        </Grid>
    )
}