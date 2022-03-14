
import { FamilyRestroomRounded } from "@mui/icons-material";
import {  createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { Component } from "react";
import agent from "../../app/api/agent";
import Countries from "./Countries";



interface basicsState {
    status: string,
    activeTable : Component | null,
    yearsList: number[],
    monthList: string[],
    LeadTimeActivities: string[],
    
}

const initialState : basicsState = {
    status: 'idle',
    activeTable: null,
    yearsList: [1399, 1400, 1401, 1402],
    monthList: ['Farvardin', 'Ordibehesht', 'Khordad', 'Tir', 'Mordad', 'Shahrivar', 'Mehr', 'Aban', 'Azar', 'Dey', 'Bahman', 'Esfand'],
    LeadTimeActivities: ['Analyze Shipment', 'Request Mofa', 'Loading', 'Shipping' , 'Customs Clearance', 'Ship to Warehouse']

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