import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { accountSlice } from "../../features/account/accountSlice";
import { basicsSlice } from "../../features/system/basicsSlice";
import { catalogSlice } from "../../features/product/catalogSlice";



export const store = configureStore({
    reducer: {
        basics: basicsSlice.reducer, 
        account: accountSlice.reducer,
        catalog: catalogSlice.reducer,
        
    }
})

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector : TypedUseSelectorHook<RootState> = useSelector;