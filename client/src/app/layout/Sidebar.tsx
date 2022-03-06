import { Drawer, Toolbar, Box, List, ListItem, ListItemIcon, ListItemText, Divider, Collapse, ListItemButton } from "@mui/material";
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import Brands from "../../features/basics/Brands";
import Categories from "../../features/basics/Categories";
import Countries from "../../features/basics/Countries";
import EarbudsIcon from '@mui/icons-material/Earbuds';
import { useState } from "react";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { useAppSelector } from "../store/configureStore";
import { NavLink } from "react-router-dom";

export default function Sidebar() {
    const [open, setOpen] = useState(-1);
    const drawerWidth = 240;
    
    const Menu = [
        {
            title: 'SYSTEM', icon: <EarbudsIcon />, subItems: [
                { title: 'Countries', path: '/countries' },
                { title: 'Brands', path: '/brands' },
                { title: 'Categories', path: '/categories' },
            ]
        },
        { title: 'Brands'},
        { title: 'Categories' },
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
            <Toolbar />
            <Box sx={{ overflow: 'auto' }}>
                <List>
                    {
                        Menu.map((item, index) => (
                            <>
                                <ListItem button key={item.title} onClick={()=>handleClick(index)}>
                                    <ListItemText primary={item.title} />
                                    {(open === index) ? <ExpandLess /> : <ExpandMore />}
                                </ListItem>
                                <Collapse in={open === index} timeout="auto" unmountOnExit>
                                    <List component="div" disablePadding>
                                        {item.subItems?.map((sub) => (
                                                <ListItem
                                                    component={NavLink} to={sub.path}
                                                    key={sub.title} 
                                                    sx={{ pl: 4, bgcolor : 'secondary' }} >
                                                    <ListItemText primary={sub.title} />
                                                </ListItem>
                                        ))}

                                    </List>
                                </Collapse>
                            </>
                        ))
                    }

                </List>
            </Box>
        </Drawer>
    );
}