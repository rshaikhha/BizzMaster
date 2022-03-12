import { Drawer, Toolbar, Box, List, ListItem, ListItemIcon, ListItemText, Divider, Collapse, ListItemButton } from "@mui/material";
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import Brands from "../../features/system/Brands";
import Categories from "../../features/system/Categories";
import Countries from "../../features/system/Countries";
import EarbudsIcon from '@mui/icons-material/Earbuds';
import { useState } from "react";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { useAppSelector } from "../store/configureStore";
import { NavLink } from "react-router-dom";

export default function Sidebar() {
    const [open, setOpen] = useState(-1);
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
        { title: 'SUPPLIERS' , subItems: [
            { title: 'Supplier List', path: '/Suppliers'},
            { title: 'Supply Lines', path: '/Supplylines'},

        ]},
    ];

    function handleClick(index: number){
        if (open === index){
            setOpen(-1)
        }
        else {
            setOpen(index)
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
        >
            <Box sx={{ overflow: 'auto', mt:8 }}>
                <List>
                    {
                        Menu.map((item, index) => (
                            <Box key = {index}>
                                <ListItem button onClick={()=>handleClick(index)}>
                                    <ListItemText primary={item.title}/>
                                    {(open === index) ? <ExpandLess /> : <ExpandMore  />}
                                </ListItem>
                                <Collapse in={open === index} timeout="auto" unmountOnExit >
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