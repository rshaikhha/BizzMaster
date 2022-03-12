import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";
import agent from "../../app/api/agent";
import { MetaData } from "../../app/models/pagination";
import { Product, ProductParams } from "../../app/models/product";
import { RootState } from "../../app/store/configureStore";

interface catalogState {
    productsLoaded: boolean;
    filtersLoaded: boolean;
    status: string;
    brands: string[];
    categories: string[];
    
    productParams: ProductParams;
    metaData: MetaData | null;

    viewMode: string;

}
const productAdapter = createEntityAdapter<Product>();

// convert productParams to URLSearchParams
function getAxiosParams(productParams: ProductParams) {
    const params = new URLSearchParams();
    params.append('pageNumber', productParams.pageNumber.toString());
    params.append('pageSize', productParams.pageSize.toString());
    params.append('orderBy', productParams.orderBy);
    if (productParams.searchTerm)
        params.append('searchTerm', productParams.searchTerm);
    if (productParams.brands)
        params.append('brands', productParams.brands.toString());
    if (productParams.types)
        params.append('categories', productParams.types.toString());
    return params;
}


export const fetchProductsAsync = createAsyncThunk<Product[],void, {state: RootState}>(
    'catalog/fetchProductsAsync',
    async (_, thunkAPI) => {
        const params = getAxiosParams(thunkAPI.getState().catalog.productParams);
        try {
            const response = await agent.Catalog.list(params);
            thunkAPI.dispatch(setMetaData(response.metaData));
            return response.items;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)

export const fetchProductAsync = createAsyncThunk<Product, number>(
    'catalog/fetchProductAsync',
    async (productId, thunkAPI) => {
        try {
            return await agent.Catalog.details(productId);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({error: error.data})
        }
    }
)

export const fetchFilters = createAsyncThunk(
    'catalog/fetchFilters',
    async (_, thunkAPI) => {
        try {
            return agent.Catalog.fetchFilters();
        } catch (error : any) {
            return thunkAPI.rejectWithValue({error: error.data});
        }
    }
)

function initParams(){
    return {
        pageNumber: 1,
        pageSize: 20,
        orderBy: 'default',
        viewMode: 'card'
    }
}

export const catalogSlice = createSlice({
    name: 'catalog',
    initialState: productAdapter.getInitialState<catalogState>({
        productsLoaded: false,
        filtersLoaded: false,
        status: 'idle',
        brands: [],
        categories: [],
        productParams: initParams(),
        metaData: null,
        viewMode: 'card'
    }),
    reducers: {
        setProductParams: (state, action) => {
            state.productsLoaded = false;
            console.log(action.payload);
            state.productParams = {...state.productParams, ...action.payload, pageNumber: 1};
        },
        setPageNumber: (state, action) =>{
            state.productsLoaded = false;
            state.productParams = {...state.productParams, ...action.payload};
        },
        setMetaData: (state, action) => {
            state.metaData = action.payload;
        },
        resetProductParams: (state) => { 
            state.productParams = initParams();
        },
        setViewMode: (state, action) => {
            state.viewMode = action.payload.viewMode;
        }
    },
    extraReducers: (builder => {
        builder.addCase(fetchProductsAsync.pending, (state) => {
            state.status = 'pendingFetchProducts'
        });
        builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
            productAdapter.setAll(state, action.payload);
            state.status = 'idle';
            state.productsLoaded = true;
        });
        builder.addCase(fetchProductsAsync.rejected, (state) => {
            state.status = 'idle';
        })

        builder.addCase(fetchProductAsync.pending, (state) => {
            state.status = 'pendingFetchProducts'
        });
        builder.addCase(fetchProductAsync.fulfilled, (state, action) => {
            productAdapter.upsertOne(state, action.payload);
            state.status = 'idle';
        });
        builder.addCase(fetchProductAsync.rejected, (state, action) => {
            state.status = 'idle';
        })

        builder.addCase(fetchFilters.pending, (state) =>{
            state.status = "pendingFetchFilters";
        })
        builder.addCase(fetchFilters.fulfilled, (state, action) =>{
            state.brands = action.payload.brands;
            state.categories = action.payload.categories;
            state.filtersLoaded = true;
            state.status = "idle";
        })
        builder.addCase(fetchFilters.rejected, (state, action) =>{
            state.status = "idle";
            console.log(action.payload);
        })

    })
})


export const productSeletors = productAdapter.getSelectors((state: RootState) => state.catalog)

export const {setProductParams, resetProductParams, setMetaData, setPageNumber, setViewMode} = catalogSlice.actions;