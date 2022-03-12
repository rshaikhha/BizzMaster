import { Grid, Typography, Divider, Button, Paper, Box, TextField, MenuItem, Avatar } from "@mui/material";
import { useState, useEffect } from "react";
import { useForm, Control, useWatch, useFieldArray } from "react-hook-form";
import { useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Product } from "../../app/models/product";
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
type FormValues = {
    SupplyLineId: number;
    year: number;
    month: number;
    items: {
        productId: string;
        quantity: number;
    }[];

};
let renderCount = 0;
const Total = ({ control }: { control: Control<FormValues> }) => {
    const formValues = useWatch({
        name: "items",
        control
    });
    const total = formValues.reduce(
        (acc, current) => acc + (current.quantity || 0),
        0
    );
    return <p>Total Quantity: {total}</p>;
};

export default function SubmitSalesForecast() {
    const { register, control, handleSubmit, formState: { errors } } = useForm<FormValues>({
        defaultValues: {
            items: []
        },
        mode: "all"
    });
    const { fields, append, remove } = useFieldArray({
        name: "items",
        control
    });
    const onSubmit = (data: FormValues) => {
        data.SupplyLineId = parseInt(id);
        const result = agent.Suppliers.setForecast(data);
        console.log(result)
        
    };

    const [single, setSingle] = useState<any>(null)
    const { id } = useParams<{ id: string }>();



    useEffect(() => {
        agent.Suppliers.lineDetails(parseInt(id)).then((res) => setSingle(res))
    }, [])


    

    if (!single) return <Loadingcomponent message='Loading Products ...' />
    const yearsList = [1399, 1400, 1401,1402];
    const monthList = ['Farvardin', 'Ordibehesht', 'Khordad', 'Tir', 'Mordad', 'Shahrivar', 'Mordad', 'Mehr', 'Aban', 'Azar', 'Dey', 'Bahman', 'Esfand'];

    const products: Product[] = single.products;

    return (

        <Paper sx ={{p:4}}>
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>

                </Avatar>
                <Typography component="h1" variant="h5">
                    Sales Forecast
                </Typography>
                <Total control={control} />
            </Box>
            <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate sx={{ mt: 1 }}>
                <Grid container spacing={2} sx={{ m: 4, alignContent: "center" }}>
                    <Grid item xs={12}>
                        <TextField
                            id="year"
                            select
                            label="Select Year"
                            {...register('year', { required: 'year is required' })}
                            error={!!errors.year}
                            helperText={errors?.year?.message}
                            placeholder="Year"
                            defaultValue= {1401}
                            sx={{ mb: 1, minWidth: '50%' }}
                        >
                            {yearsList.map((option) => (
                                <MenuItem key={option} value={option}>
                                    {option}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            id="month"
                            select
                            label="Select Month"
                            {...register('month', { required: 'month is required' })}
                            error={!!errors.month}
                            helperText={errors?.month?.message}
                            placeholder="Month"
                            sx={{ mb: 1, minWidth: '50%' }}
                            defaultValue= {1}
                        >
                            {monthList.map((option, index) => (
                                <MenuItem key={option} value={index + 1}>
                                    {option}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>

                </Grid>

                {fields.map((field, index) => {
                    return (
                        <div key={`div.${field.id}`}>
                            <section className={"section"} key={`sec.${field.id}`}>
                                <Grid container spacing={2} sx={{ ml: 4, alignContent: "center" }}>
                                    <Grid item xs={6}>
                                        <TextField
                                            fullWidth
                                            placeholder="productId"
                                            {...register(`items.${index}.productId` as const, {
                                                required: true
                                            })}
                                            className={errors?.items?.[index]?.productId ? "error" : ""}
                                            defaultValue={field.productId}
                                            select
                                            //label="Product"
                                            sx={{ mb: 1, minWidth: '50%' }}
                                        >
                                            {products.map((option) => (
                                                <MenuItem key={option.id} value={option.id}>
                                                    {option.description}
                                                </MenuItem>
                                            ))}
                                        </TextField>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <TextField
                                        fullWidth
                                            placeholder="quantity"
                                            type="number"
                                            inputProps={{ min: "0", max: "10000", step: "10" }}
                                            {...register(`items.${index}.quantity` as const, {
                                                valueAsNumber: true,
                                                required: true
                                            })}
                                            className={errors?.items?.[index]?.quantity ? "error" : ""}
                                            defaultValue={field.quantity}
                                        >
                                        </TextField>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <Button variant="text" color="error" onClick={() => remove(index)}>
                                            <HighlightOffIcon />
                                        </Button>
                                    </Grid>
                                </Grid>
                            </section>
                        </div>
                    );
                })}

                <Grid container spacing={2} sx={{ ml: 4, mb: 4, alignContent: "center" }}>
                    <Grid item xs={12}>
                        <Button variant="text" color="primary" onClick={() => append({ productId: "", quantity: 0, })}>
                            <AddCircleOutlineIcon />
                        </Button>
                    </Grid>
                    <Grid item xs={6}>
                        <Button variant="contained" color="primary" type="submit" fullWidth>
                            SUBMIT
                        </Button>
                    </Grid>
                </Grid>
            </Box>

        </Paper>
    )
}