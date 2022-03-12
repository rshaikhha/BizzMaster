import types from "@emotion/styled";
import { Grid, Paper, ButtonGroup, Button } from "@mui/material";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import agent from "../../app/api/agent";
import AppPagination from "../../app/components/AppPagination";
import CheckBoxButtons from "../../app/components/CheckBoxButtons";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import SimpleTable from "../shared/SimpleTable";
import { fetchFilters, fetchProductsAsync, productSelectors, setPageNumber, setProductParams } from "./catalogSlice";
import ProductSearch from "./ProductSearch";
export default function Products() {

    const products = useSelector(productSelectors.selectAll);
    const { productsLoaded, filtersLoaded, brands, categories, productParams, metaData, viewMode } = useAppSelector(state => state.catalog);
    const dispatch = useAppDispatch();

    const sortOptions = [
        { value: 'default', label: 'default' },
        { value: 'PartNumber', label: 'Part Number' },
        { value: 'Title', label: 'Title' }

    ]

    useEffect(() => {
        if (!filtersLoaded) dispatch(fetchFilters());
    }, [filtersLoaded, dispatch])

    useEffect(() => {
        if (!productsLoaded) dispatch(fetchProductsAsync());
    }, [productsLoaded, dispatch])

    if (!filtersLoaded) return <Loadingcomponent message='Loading Products ...' />


    const title = 'Products';
    const columns = [
         
        { header: 'Title', accessor: 'title'},
        { header: 'Part Number', accessor: 'partNumber'},
        { header: 'Description', accessor: 'description'},
        { header: 'Category', accessor: 'category'},
        { header: 'Brand', accessor: 'brand'},

    ];

    

    const tableprops = {list : products, title, columns, 
        startIndex : (((metaData?.currentPage || 1) - 1) * (metaData?.pageSize || 0)) + 1,
        detailsAddress : "Products"

    }
    return (
        <>
            <Grid container spacing={4}>
                <Grid item xs={2} >
                    <Paper sx={{ mb: 1 }}>
                        <ProductSearch />
                    </Paper>
                    <Paper sx={{ mb: 1, p: 1 }}>
                        <RadioButtonGroup
                            title='Sort'
                            selectedValue={productParams.orderBy}
                            options={sortOptions}
                            onChange={(e) => dispatch(setProductParams({ orderBy: e.target.value }))}
                        />
                    </Paper>
                    <Paper sx={{ mb: 1, p: 1 }}>
                        <CheckBoxButtons
                            title='Brand'
                            items={brands}
                            checked={productParams.brands}
                            onChange={(checkedItems: string[]) => dispatch(setProductParams({ brands: checkedItems }))}
                        />
                    </Paper>
                    <Paper sx={{ mb: 1, p: 1 }}>
                        <CheckBoxButtons
                            title='Category'
                            items={categories}
                            checked={productParams.types}
                            onChange={(checkedItems: string[]) => dispatch(setProductParams({ types: checkedItems }))}
                        />
                    </Paper>
                </Grid>
                <Grid item xs={9}>
                    <SimpleTable {...tableprops} />
                    
                </Grid>
                <Grid item xs={9}>
                    {metaData &&
                        <AppPagination
                            metaData={metaData}
                            onPageChange={(page: number) => dispatch(setPageNumber({ pageNumber: page }))}
                        />
                    }
                </Grid>
            </Grid>


        </>)


}