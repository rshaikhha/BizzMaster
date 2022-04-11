import { Typography } from "@mui/material";
import { Redirect } from "react-router-dom";
import { useAppSelector } from "../../app/store/configureStore";

export default function Dashboard(){
    const {user} = useAppSelector(state => state.account);




    if(!user?.token) return <Redirect push to="/login" />
    return (
        <Typography
            variant="h1"
            gutterBottom
            style={{ fontFamily: "'Noto Sans Arabic', sans-serif", color: "pink" }}
        >
            داشبورد فروش
        </Typography>
    )




}


