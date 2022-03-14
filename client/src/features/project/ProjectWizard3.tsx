import { Grid, Typography, Divider, Button, Paper, Box, TextField, MenuItem, duration } from "@mui/material";
import { useState, useEffect } from "react";
import { useForm, Control, useWatch, useFieldArray, Controller } from "react-hook-form";
import { NavLink, useHistory, useParams } from "react-router-dom";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import { Product } from "../../app/models/product";
import HighlightOffIcon from '@mui/icons-material/HighlightOff';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import CloudDownloadIcon from '@mui/icons-material/CloudDownload';
import StoreIcon from '@mui/icons-material/Store';
import { useAppSelector } from "../../app/store/configureStore";
import CalculateIcon from '@mui/icons-material/Calculate';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import { DatePicker } from '@mui/lab';
import { TodayTwoTone } from "@mui/icons-material";


type FormValues = {
    title: string;
    supplyLineId: number;
    orderId: number;
    startDate: string;
    finishDate: string;
    items: {
        title: string;
        duration: number;
    }[];

};
const Total = ({ control }: { control: Control<FormValues> }) => {
    const formValues = useWatch({
        name: "items",
        control
    });
    const total = formValues.reduce(
        (acc, current) => acc + (current.duration || 0),
        0
    );
    return <p>Total Duration: {total}</p>;
};



export default function ProjectWizard3() {
    const { id, orderId } = useParams<{ id: string, orderId: string }>();
    const [singleId, setSingleId] = useState<string | null>(null)
    const [single, setSingle] = useState<any>(null)
    const [lines, setLines] = useState<any[]>([])
    const [products, setProducts] = useState<Product[]>([])
    const [loaded, setLoaded] = useState<Boolean>(false)
    const getDate = (days: number = 0) => {
        var t = new Date();
        t.setDate(t.getDate() + days)
        var dd = String(t.getDate()).padStart(2, '0');
        var mm = String(t.getMonth() + 1).padStart(2, '0'); //January is 0!
        var yyyy = t.getFullYear();

        return (yyyy + '/' + mm + '/' + dd)
    }
    const history = useHistory();

    const { LeadTimeActivities } = useAppSelector(state => state.basics)
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
        data.supplyLineId = parseInt(id);
        data.orderId = parseInt(orderId);
        agent.Projects.post(data).then(() => history.push(`/projects`))
            .catch((e) => {
                setError("items", {
                    type: "manual",
                    message: e.error.message,
                });
            });
    };

    const onLoad = (data: FormValues) => {
        reset({
            items: []
        })
        data.supplyLineId = parseInt(id);
        agent.Suppliers.LeadTimes(data.supplyLineId).then((res) => {
            console.log(res)
            if (res) {
                reset({
                    startDate: getDate(),
                    finishDate: getDate(res.totalDuration),
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
            supplyLineId: parseInt(id),
            items: []
        })
        setLoaded(false);
        agent.Suppliers.lines().then((res) => { setLines(res) })
        if (singleId == null) setSingleId(id)
        agent.Suppliers.lineDetails(parseInt(singleId || id)).then((res) => {
            setSingle(res);
        })
        setLoaded(true);

    }, [id])


    if (!single || !lines) return <Loadingcomponent />


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
                <AccessTimeIcon fontSize="large" />
                {/* <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>

                </Avatar> */}
                <Typography component="h1" variant="h5">
                    Project Lead Time
                </Typography>
                <Typography component="h2" variant="h5">{single.title}</Typography>
                <Total control={control} />
            </Box>
            <Box component="form" onSubmit={handleSubmit(onSubmit)} noValidate sx={{ mt: 1 }}>
                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }}>
                    <Grid item xs={3}>
                        <TextField
                            fullWidth
                            placeholder="title"
                            {...register("title")}
                            className={errors?.title ? "error" : ""}
                            //label="Start Date"
                            sx={{ mb: 1, minWidth: '50%' }}
                        >
                        </TextField>
                    </Grid>



                </Grid>
                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }}>
                    <Grid item xs={3}>
                        <TextField
                            fullWidth
                            placeholder="start Date"
                            {...register("startDate")}
                            className={errors?.startDate ? "error" : ""}
                            //label="Start Date"
                            sx={{ mb: 1, minWidth: '50%' }}
                        >
                        </TextField>
                    </Grid>

                    <Grid item xs={3}>
                        <TextField
                            fullWidth
                            placeholder="Finsish Date"
                            {...register("finishDate")}
                            className={errors?.finishDate ? "error" : ""}
                            //label="Finish Date"
                            sx={{ mb: 1, minWidth: '50%' }}

                        >
                        </TextField>

                    </Grid>

                </Grid>
                <Grid container direction="row" alignItems="center" spacing={2} sx={{ m: 1, alignContent: "center" }}>
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
                                            placeholder="Title"
                                            {...register(`items.${index}.title` as const, {
                                                required: true
                                            })}
                                            className={errors?.items?.[index]?.title ? "error" : ""}
                                            defaultValue={field.title}
                                            select
                                        //label="Product"
                                        // sx={{ mb: 1, minWidth: '50%' }}
                                        >
                                            {LeadTimeActivities.map((option) => (
                                                <MenuItem key={option} value={option}>
                                                    {option}
                                                </MenuItem>
                                            ))}
                                        </TextField>
                                    </Grid>
                                    <Grid item xs={3}>
                                        <TextField
                                            fullWidth
                                            placeholder="Duration"
                                            type="number"
                                            inputProps={{ min: "0", max: "30", step: "1" }}
                                            {...register(`items.${index}.duration` as const, {
                                                valueAsNumber: true,
                                                required: true
                                            })}
                                            className={errors?.items?.[index]?.duration ? "error" : ""}
                                            defaultValue={field.duration}
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
                        <Button variant="text" color="primary" onClick={() => append({ title: "", duration: 0, })}>
                            <AddCircleOutlineIcon />
                        </Button>
                    </Grid>
                </Grid>
                <Grid container spacing={2} sx={{ ml: 4, mb: 4, alignContent: "center" }}>
                    <Grid item xs={6}>
                        <Button variant="contained" color="primary" type="submit" fullWidth>
                            SUBMIT
                        </Button>
                    </Grid>
                    <Grid item xs={2}>
                        <Button variant="contained" color="error" fullWidth component={NavLink} to={`/ProjectWizard2/${id}`}>
                            CANCEL
                        </Button>
                    </Grid>
                </Grid>
            </Box>

        </Paper>
    )
}