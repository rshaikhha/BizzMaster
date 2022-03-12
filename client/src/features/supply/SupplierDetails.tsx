import { Grid, Typography, Divider, TableContainer, Table, TableBody, TableRow, TableCell, TextField } from "@mui/material";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import NotFound from "../error/NotFound";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";

export default function SupplierDetails() {
    
    const [supplier, setSupplier] = useState<any>(null)
    const {id} = useParams<{id: string}>();
    
    

    useEffect(() => {
        agent.Suppliers.details(parseInt(id)).then((res) => setSupplier(res))
    }, [])

    

    const columns = [
         
        { header: 'Title', accessor: 'title'},
        { header: 'Full Title', accessor: 'fullTitle'},
        { header: 'Country', accessor: 'country'},
        { header: 'Email', accessor: 'email'},
        { header: 'Address', accessor: 'address'},
        { header: 'Website', accessor: 'website'},

    ];

    if (!supplier) return <Loadingcomponent message='Loading Products ...' />
    return (
        <Grid container spacing={6}>
            <Grid item xs={6}>
                <Typography variant='h4'>{supplier.title}</Typography>
                <Divider sx={{mb: 2}} />
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
                            {/* <TableRow>
                                <TableCell>title</TableCell>
                                <TableCell>{product.title}</TableCell>
                            </TableRow>    
                            <TableRow>
                                <TableCell>Description</TableCell>
                                <TableCell>{product.description}</TableCell>
                            </TableRow>  
                            <TableRow>
                                <TableCell>Category</TableCell>
                                <TableCell>{product.category}</TableCell>
                            </TableRow>  
                            <TableRow>
                                <TableCell>Brand</TableCell>
                                <TableCell>{product.brand}</TableCell>
                            </TableRow>  
                            <TableRow>
                                <TableCell>Part Number</TableCell>
                                <TableCell>{product.partNumber}</TableCell>
                            </TableRow> */}
                        </TableBody>
                    </Table>
                </TableContainer>
                {/* <Grid container spacing={2}>
                    <Grid item xs={6}>
                        <TextField 
                            variant='outlined'
                            type='number'
                            label='Quantity in Cart'
                            fullWidth
                            value={quantity}
                            onChange={handleInputChange}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <LoadingButton
                            disabled={item?.quantity === quantity}
                            loading={status.includes('pending')}
                            onClick={handleUpdateCart}
                            sx={{height: '55px'}}
                            color='primary'
                            size='large'
                            variant='contained'
                            fullWidth
                        >
                            {item ? 'Update Quantity' : 'Add to Cart'}
                        </LoadingButton>
                    </Grid>
                </Grid> */}
            </Grid>
        </Grid>
    )
}