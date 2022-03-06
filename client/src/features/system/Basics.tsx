import { Button, Grid, List, ListItem, ListItemButton, ListItemIcon, ListItemText, ListSubheader, Paper } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { setActiveTable } from "./basicsSlice";
import Brands from "./Brands";
import Categories from "./Categories";
import Countries from "./Countries";

export default function Basics() {

    const dispatch = useAppDispatch();
    const { activeTable } = useAppSelector(state => state.basics)
    
    const tables = [
        { header: 'Countries', accessor: <Countries /> },
        { header: 'Brands', accessor: <Brands /> },
        { header: 'Categories', accessor: <Categories /> },
    ];

    


    return (
        <>
            <Grid container spacing={4}>
                <Grid item xs={3}>
                    <Paper sx={{ mb: 2}}>
                        <List
                            
                            subheader={<ListSubheader sx={{ bgcolor: 'grey.500' }} >Select Table</ListSubheader>}>
                            {tables.map((item) => (
                                <ListItem disablePadding >
                                    <ListItemButton  onClick={() => dispatch(setActiveTable(item.accessor))}>
                                        <ListItemText primary={item.header} />
                                    </ListItemButton>
                                </ListItem>
                            ))}
                        </List>
                    </Paper>
                </Grid>
                <Grid item xs={9}>
                   {activeTable}

                </Grid>

            </Grid>

        </>
    )
}