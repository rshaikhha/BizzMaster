import { Button, Typography } from "@mui/material";
import { useState } from "react";
import agent from "../../app/api/agent";

export default function HomePage(){

    const [res, setRes] = useState<string>('');
    
    
    return (
        <>
        <Typography variant='h2'>
            Business Gate Group
        </Typography>
        </>
    )
}