import { Grid, Typography, Divider, TableContainer, Table, TableBody, TableRow, TableCell, TextField, Paper, Button, Card, CardActions, CardContent, CardMedia, CardHeader, Avatar, IconButton } from "@mui/material";
import { useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import NotFound from "../error/NotFound";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { red } from "@mui/material/colors";
import PhoneIphoneIcon from '@mui/icons-material/PhoneIphone';
import WhatsAppIcon from '@mui/icons-material/WhatsApp';
import EmailIcon from '@mui/icons-material/Email';
import { WhatsappOutlined } from "@mui/icons-material";

export default function SupplierDetails() {

    const [supplier, setSupplier] = useState<any>(null)
    const { id } = useParams<{ id: string }>();
    const history = useHistory();


    useEffect(() => {
        agent.Suppliers.details(parseInt(id)).then((res) => setSupplier(res))
    }, [])



    const columns = [

        { header: 'Title', accessor: 'title' },
        { header: 'Full Title', accessor: 'fullTitle' },
        { header: 'Country', accessor: 'country' },
        { header: 'Email', accessor: 'email' },
        { header: 'Address', accessor: 'address' },
        { header: 'Website', accessor: 'website' },

    ];

    if (!supplier) return <Loadingcomponent message='Loading Products ...' />
    console.log(supplier.contacts[0])
    return (
        <Grid container spacing={6} sx={{ m: 4 }}>
            <Grid item xs={8} component={Paper}>
                <Typography variant='h4'>{supplier.title}</Typography>
                <Divider sx={{ mb: 2 }} />
                {/* <Typography variant='h4' color='secondary'>${(product.description / 100).toFixed(2)}</Typography> */}
                <TableContainer>
                    <Table>
                        <TableBody>
                            {columns.map((item) => (
                                <TableRow>
                                    <TableCell>{item.header}</TableCell>
                                    <TableCell>{(supplier)[item.accessor]}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>

            </Grid>
            <Grid item xs={8} display="flex">
                {supplier.contacts.map((item: any) => (
                    <Card sx={{ maxWidth: 350, m: 2 }}>
                        <CardHeader
                            avatar={
                                <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
                                    {item.firstName[0]}
                                </Avatar>
                            }
                            title={item.firstName + " " + item.lastName}
                            subheader={item.position}
                        />
                        <CardContent>
                            <Grid container direction="row" alignItems="center" sx={{ mb: 1 }} component = "button" 
                            onClick={(e : any) => { 
                                window.location.href = `mailto:${item.email}`;
                                }}>
                                <EmailIcon /> <Typography variant="body2">{item.email}</Typography>
                            </Grid>
                            <Grid container direction="row" alignItems="center" sx={{ mb: 1 }} component = "button">
                                <PhoneIphoneIcon /> <Typography variant="body2">{item.mobile}</Typography>
                            </Grid>
                            <Grid container direction="row" alignItems="center" sx={{ mb: 1 }} component = "button">
                                <WhatsappOutlined /> <Typography variant="body2">{item.whatsApp}</Typography>
                            </Grid>
                            

                        </CardContent>
                        <CardActions>
                            <Button size="small">Details</Button>
                        </CardActions>
                    </Card>
                ))

                }
            </Grid>
        </Grid>
    )
}