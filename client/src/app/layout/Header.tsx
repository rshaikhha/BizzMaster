
import { AppBar, Avatar, Box, Collapse, Drawer, IconButton, List, ListItem, ListItemText, Menu, Switch, Toolbar, Typography } from "@mui/material";
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import MenuIcon from '@mui/icons-material/Menu';

import SignedInMenu from "./SignedInMenu";
import { useState } from "react";
import { ExpandLess, ExpandMore, Image } from "@mui/icons-material";
import { rightMenuItems, SidebarItems } from "./MenuItems";
import LoginIcon from '@mui/icons-material/Login';

interface Props {
    darkMode: boolean;
    handleThemeChange: () => void;
}


export default function Header({ darkMode, handleThemeChange }: Props) {
    const [isDrawerOpen, setIsDrawerOpen] = useState(false);
    const [openitem, setOpenitem] = useState(-1);
    //const { basket } = useAppSelector(state => state.basket);
    const { user } = useAppSelector(state => state.account);
    //const itemCount = basket?.items.reduce((sum, item) => sum + item.quantity, 0)
    //const logo = require('./logo.png');
    const logo = './logo.png';

    const navStyles = {
        textDecoration: 'none',
        color: 'inherit',
        typography: 'body2',
        '&:hover': {
            color: 'grey.500'
        },
        '&.active': {
            color: 'text.secondary'
        },
        ml: 1
    }

    function handleClick(index: number) {
        if (openitem === index) {
            setOpenitem(-1)
        }
        else {
            setOpenitem(index)
        }
    }

    const drawerWidth = 240;



    return (
        <AppBar position='fixed' sx={{ mb: 6, zIndex: (theme) => theme.zIndex.drawer + 1 }}
        >
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', direction: 'rtl' }}>
                <Box id="MenuIcon" display='flex' alignItems='center'>

                    <IconButton
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        onClick={() => setIsDrawerOpen(!isDrawerOpen)}
                    >
                        <MenuIcon />
                    </IconButton>
                </Box>
                <Box id="Logo" display='flex' alignItems='center'>
                    <IconButton
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        component={NavLink}
                        to='/'
                    >
                        <Box
                            component="img"
                            alt="User Avatar"
                            src={logo}
                            sx={{ maxHeight: 35 }}
                          />
                    </IconButton>
                </Box>

                <Box display='flex' alignItems='center'>
                    {/* <IconButton component={Link} to='/Basket' size='large' sx={{ color: 'inherit' }}>
                        <Badge badgeContent={itemCount} color='secondary'>
                            <ShoppingCart />
                        </Badge>
                    </IconButton> */}
                    {user?.token ? (
                        <SignedInMenu />
                    ) : (
                        <Box display='flex' alignItems='center'>

                            <IconButton
                                edge="start"
                                color="inherit"
                                aria-label="menu"
                                component={NavLink}
                                to='/login'
                            >
                                <LoginIcon />
                            </IconButton>

                        </Box>

                    )
                    }

                </Box>
                {user?.token && (

                <Drawer
                    anchor = 'right'
                    sx={{
                        width: drawerWidth,
                        flexShrink: 0,
                        [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
                    }}
                    open={isDrawerOpen}
                    onClose={() => setIsDrawerOpen(false)}
                >
                    <Box sx={{ overflow: 'auto', mt: 8, direction: 'rtl' }}>
                        <List>
                            {
                                SidebarItems.map((item, index) => (
                                    <Box key={index} >
                                        <ListItem button onClick={() => handleClick(index)} dir = 'rtl'>
                                            <ListItemText primary={item.title} dir= 'rtl' />
                                            {(openitem === index) ? <ExpandLess /> : <ExpandMore />}
                                        </ListItem>
                                        <Collapse in={openitem === index} timeout="auto" unmountOnExit >
                                            <List component="div" disablePadding >
                                                {item.subItems?.map((sub, subindex) => (
                                                    <ListItem
                                                        component={NavLink} to={sub.path}
                                                        key={sub.path}
                                                        sx={{ pl: 4, bgcolor: 'secondary' }}
                                                        onClick={() => setIsDrawerOpen(false)}
                                                    >
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
                )}


            </Toolbar>
        </AppBar>
    )
}