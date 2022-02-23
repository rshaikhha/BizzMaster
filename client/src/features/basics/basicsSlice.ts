import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import agent from "../../app/api/agent";


const initialState = {

};

const fetchCountries = createAsyncThunk(
    'basics/countries',
    async () => {
        return await agent.countries.list();
    }
)


export const basicsSlice = createSlice({
    name: 'basics',
    initialState,
    reducers: {
        getCountries: ()=> {

        }
    },
    extraReducers: (Builder => 
        Builder.addCase(fetchCountries.fulfilled, (state, action) => {
            
        })
        )
    
})