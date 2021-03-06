import { Drawer, Box, List, ListItem, ListItemText, Collapse } from "@mui/material";
import { useState } from "react";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { NavLink } from "react-router-dom";

export default function Sidebar() {
    const [open, setOpen] = useState(false);
    const [openitem, setOpenitem] = useState(-1);
    const drawerWidth = 240;
    
    const Menu = [
        { title: 'SYSTEM', subItems: [
                { title: 'Countries', path: '/countries' },
                { title: 'Brands', path: '/brands' },
                { title: 'Usage Types', path: '/usagetypes'},
                { title: 'Master Systems', path: '/mastersystems'},
                { title: 'Categories', path: '/categories' },
        ]},
        { title: 'CARS' , subItems: [
            { title: 'Car Finder', path: '/CarFinder'},
            { title: 'Brands', path: '/CarBrands' },
            { title: 'Platforms', path: '/platforms' },
            { title: 'Cars', path: '/Cars' }
        ]},
        { title: 'PRODUCTS' , subItems: [
            { title: 'Poduct List', path: '/Products'},

        ]},
        { title: 'COMMERCIAL' , subItems: [
            { title: 'Supplier List', path: '/Suppliers'},
            { title: 'Commercial Cards', path: '/commercialCards'},
            { title: 'Order Registration', path: '/OrderRegistrations'},

        ]},
        { title: 'PLANNING' , subItems: [
            { title: 'Supply Lines', path: '/Supplylines'},

        ]},

        { title: 'PROJECT' , subItems: [
            { title: 'Projects', path: '/Projects'},

        ]},
        { title: 'SALES' , subItems: [
            { title: 'CUSTOMERS', path: '/SubmitSalesForecast'},
        ]},
    ];

    function handleClick(index: number){
        if (openitem === index){
            setOpenitem(-1)
        }
        else {
            setOpenitem(index)
        }
    }


    return (

        <Drawer
            variant="permanent"
            sx={{
                width: drawerWidth,
                flexShrink: 0,
                [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
            }}
            open = {open}
        >
            <Box sx={{ overflow: 'auto', mt:8 }}>
                <List>
                    {
                        Menu.map((item, index) => (
                            <Box key = {index}>
                                <ListItem button onClick={()=>handleClick(index)}>
                                    <ListItemText primary={item.title}/>
                                    {(openitem === index) ? <ExpandLess /> : <ExpandMore  />}
                                </ListItem>
                                <Collapse in={openitem === index} timeout="auto" unmountOnExit >
                                    <List component="div" disablePadding >
                                        {item.subItems?.map((sub, subindex) => (
                                                <ListItem
                                                    component={NavLink} to={sub.path}
                                                    key={sub.path} 
                                                    sx={{ pl: 4, bgcolor : 'secondary' }} >
                                                    <ListItemText primary={sub.title} />
                                                </ListItem>
                                        ))}

                                    </List>
                                </Collapse>
                            </Box>
                        ))
                    }

                </List>
            </Box>
        </Drawer>
    );
}