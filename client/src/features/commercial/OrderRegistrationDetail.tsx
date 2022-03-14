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

export default function OrderRegistrationDetail() {

    const [reg, setReg] = useState<any>(null)
    const { id } = useParams<{ id: string }>();
    const history = useHistory();


    useEffect(() => {
        agent.Commercial.Get(parseInt(id)).then((res) => setReg(res))
    }, [])



    const columns = [

        { header: 'Title', accessor: 'title'},
        { header: 'Card', accessor: 'commercialCardTitle'},
        { header: 'Document', accessor: 'documentNumber'},
        { header: 'Registration', accessor: 'registrationNumber'},
        { header: 'Currency', accessor: 'currency'},
        { header: 'Amount', accessor: 'amount'},
        { header: 'Unit', accessor: 'unit'},
        { header: 'Quantity', accessor: 'quantity'},
        { header: 'Issue Date', accessor: 'iDate'},
        { header: 'Validity Date', accessor: 'vDate'},
        { header: 'Status', accessor: 'orderRegistrationStatus'},
        { header: 'Categories', accessor: 'categories'},

    ];

    if (!reg) return <Loadingcomponent />
    return (
        <Grid container spacing={6} sx={{ m: 4 }}>
            <Grid item xs={8} component={Paper}>
                <Typography variant='h2'>Order Registration</Typography>
                <Typography variant='h4'>{reg.title}</Typography>
                <Divider sx={{ mb: 2 }} />
                {/* <Typography variant='h4' color='secondary'>${(product.description / 100).toFixed(2)}</Typography> */}
                <TableContainer>
                    <Table>
                        <TableBody>
                            {columns.map((item) => (
                                <TableRow>
                                    <TableCell>{item.header}</TableCell>
                                    <TableCell>{(reg)[item.accessor]}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>

            </Grid>
            
        </Grid>
    )
}