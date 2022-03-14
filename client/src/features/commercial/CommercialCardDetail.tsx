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

export default function CommercialCardDetail() {

    const [comcard, setComcard] = useState<any>(null)
    const { id } = useParams<{ id: string }>();
    const history = useHistory();


    useEffect(() => {
        agent.Commercial.Card(parseInt(id)).then((res) => setComcard(res))
    }, [])



    const columns = [

        { header: 'Title', accessor: 'title'},
        { header: 'Description', accessor: 'description'},
        { header: 'Issue Date', accessor: 'iDate'},
        { header: 'Validity Date', accessor: 'vDate'},
        { header: 'Card Holder', accessor: 'fullName'},

    ];

    if (!comcard) return <Loadingcomponent />
    return (
        <Grid container spacing={6} sx={{ m: 4 }}>
            <Grid item xs={8} component={Paper}>
                <Typography variant='h2'>Commercial Card</Typography>
                <Typography variant='h4'>{comcard.title}</Typography>
                <Divider sx={{ mb: 2 }} />
                {/* <Typography variant='h4' color='secondary'>${(product.description / 100).toFixed(2)}</Typography> */}
                <TableContainer>
                    <Table>
                        <TableBody>
                            {columns.map((item) => (
                                <TableRow>
                                    <TableCell>{item.header}</TableCell>
                                    <TableCell>{(comcard)[item.accessor]}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>

            </Grid>
            
        </Grid>
    )
}