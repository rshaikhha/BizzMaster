import { Grid, Typography, Divider, Button, Paper, Box, TextField, MenuItem } from "@mui/material";
import { useState, useEffect } from "react";
import { useForm, Control, useWatch, useFieldArray } from "react-hook-form";
import { useHistory, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Product } from "../../app/models/product";
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import CloudDownloadIcon from '@mui/icons-material/CloudDownload';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
type FormValues = {
    supplyLineId: number;
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

export default function SupplyLineChooser() {
    const { id } = useParams<{ id: string }>();
    const [singleId, setSingleId] = useState<number>(parseInt(id))
    const [single, setSingle] = useState<any>(null)
    const [lines, setLines] = useState<any[]>([])
    const [products, setProducts] = useState<Product[]>([])
    const [loaded, setLoaded] = useState<Boolean>(false)

    const history = useHistory();
    const { register, control, handleSubmit, formState: { errors }, reset, setError } = useForm<FormValues>({
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
        console.log(data.supplyLineId)
        // data.supplyLineId = parseInt(id);
        agent.Suppliers.setForecast(data).then(() => history.push(`/SalesForecast/${id}`))
            .catch((e) => {
                setError("items", {
                    type: "manual",
                    message: e.error.message,
                });
            });
    };

    const onLoad = (data: FormValues) => {
        data.supplyLineId = parseInt(id);
        console.log('xx')
        agent.Suppliers.getForecast(data.supplyLineId, data.year, data.month).then((res) => {
            console.log(res)
            if (res) {
                reset({
                    items: res.items
                })
            } else {
                console.log('not found')
                reset({
                    items: []
                })
            }
        });
    };




    useEffect(() => {
        reset({
            items: []
        })
        setLoaded(false);
        console.log('effect')
        agent.Suppliers.lines().then((res)=> {setLines(res)})
        console.log(lines)
        
        if(!singleId) setSingleId(1)
        agent.Suppliers.lineDetails(singleId).then((res) => {
            console.log(res)
            setSingle(res);
            setProducts(res.products);
        })
        setLoaded(true);
        
    }, [singleId])




    if (!single || !lines || !products) return <Loadingcomponent message='Loading Products ...' />
    const yearsList = [1399, 1400, 1401, 1402];
    const monthList = ['Farvardin', 'Ordibehesht', 'Khordad', 'Tir', 'Mordad', 'Shahrivar', 'Mehr', 'Aban', 'Azar', 'Dey', 'Bahman', 'Esfand'];

    

    return (

        <Paper sx={{ p: 4 }}>
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <ShoppingCartIcon fontSize="large" />
                {/* <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>

                </Avatar> */}
                <Typography component="h1" variant="h5">
                    Sales Forecast
                </Typography>
                <Total control={control} />
            </Box>
            <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate sx={{ mt: 1 }}>
                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }}>
                    <Grid item xs={6}>
                        <TextField
                            id="supplyLineId"
                            select
                            label="Select Supply Line"
                            {...register('supplyLineId', { required: 'Supply Line is required' })}
                            error={!!errors.supplyLineId}
                            helperText={errors?.supplyLineId?.message}
                            placeholder="Supply Line"
                            defaultValue={1}
                            value = {single.id}
                            fullWidth
                            onChange={(event) => setSingleId(parseInt(event.target.value))}
                            // disabled = {id != null}
                        >
                            {lines.map((option) => (
                                <MenuItem key={option.id} value={option.id}>
                                    {option.title}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>

                </Grid>
                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }}>
                    <Grid item xs={3}>
                        <TextField
                            id="year"
                            select
                            label="Select Year"
                            {...register('year', { required: 'year is required' })}
                            error={!!errors.year}
                            helperText={errors?.year?.message}
                            placeholder="Year"
                            defaultValue={1401}
                            // sx={{ m: 1, minWidth: '50%' }}
                            fullWidth
                        >
                            {yearsList.map((option) => (
                                <MenuItem key={option} value={option}>
                                    {option}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item xs={3}>
                        <TextField
                            id="month"
                            select
                            label="Select Month"
                            {...register('month', { required: 'month is required' })}
                            error={!!errors.month}
                            helperText={errors?.month?.message}
                            placeholder="Month"
                            // sx={{ m: 1, minWidth: '50%' }}
                            defaultValue={1}
                            fullWidth
                        >
                            {monthList.map((option, index) => (
                                <MenuItem key={option} value={index + 1}>
                                    {option}
                                </MenuItem>
                            ))}

                        </TextField>

                    </Grid>
                    <Grid item xs={3}>

                        <Button variant="outlined" color="primary" onClick={handleSubmit(onLoad)}>
                            <CloudDownloadIcon />
                            <Typography variant="body2" sx={{ m: 1 }}>Load Data</Typography>

                        </Button>
                    </Grid>

                </Grid>

                {fields.map((field, index) => {
                    return (
                        <div key={`div.${field.id}`}>
                            <section className={"section"} key={`sec.${field.id}`}>
                                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }} >
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
                                        // sx={{ mb: 1, minWidth: '50%' }}
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