
import { FamilyRestroomRounded } from "@mui/icons-material";
import {  createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Component } from "react";
import agent from "../../app/api/agent";
import Countries from "./Countries";



interface basicsState {
    status: string,
    activeTable : Component | null,
    
}

const initialState : basicsState = {
    status : 'idle',
    activeTable : null,

}




export const basicsSlice = createSlice({
    name: 'basics',
    initialState,
    reducers: {
        setActiveTable: (state, action) => {
            state.activeTable = action.payload;
        },
        
    },
    
    
    
})

export const {setActiveTable} = basicsSlice.actions;