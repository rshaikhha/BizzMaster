import { Grid, Typography, Divider, TableContainer, Table, TableBody, TableRow, TableCell, TextField } from "@mui/material";
import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { useAppSelector, useAppDispatch } from "../../app/store/configureStore";
import { fetchProductAsync, productSelectors  } from "./catalogSlice";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import NotFound from "../error/NotFound";

export default function ProductDetails() {
    
    const dispatch = useAppDispatch();
    const {id} = useParams<{id: string}>();
    const product = useAppSelector(state => productSelectors.selectById(state, id));
    const {status: productStatus} = useAppSelector(state => state.catalog);
    

    useEffect(() => {
        if (!product) dispatch(fetchProductAsync(parseInt(id)))
    }, [id, dispatch, product]);


    if (productStatus.includes('pending')) return <Loadingcomponent message='Loading product...' />

    if (!product) return <NotFound />

    const columns = [
         
        { header: 'Title', accessor: 'title'},
        { header: 'Part Number', accessor: 'partNumber'},
        { header: 'Description', accessor: 'description'},
        { header: 'Category', accessor: 'category'},
        { header: 'Brand', accessor: 'brand'},
        { header: 'Item Per Set', accessor: 'itemPerSet'},
        { header: 'Volume (L)', accessor: 'itemVolume'},
        { header: 'Weight (G)', accessor: 'itemWeight'},

    ];


    return (
        <Grid container spacing={6}>
            <Grid item xs={6}>
                <img src={product.pictureUrl || '/Images/noimage.png'} alt={product.title} style={{width: '100%'}} />
            </Grid>
            <Grid item xs={6}>
                <Typography variant='h4'>{product.title}</Typography>
                <Divider sx={{mb: 2}} />
                {/* <Typography variant='h4' color='secondary'>${(product.description / 100).toFixed(2)}</Typography> */}
                <TableContainer>
                    <Table>
                        <TableBody>
                            {columns.map((item) => (
                                <TableRow>
                                    <TableCell>{item.header}</TableCell>
                                    <TableCell>{(product as any)[item.accessor]}</TableCell>
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