
import { AppBar, Box, List, ListItem, Switch, Toolbar, Typography } from "@mui/material";
import { NavLink } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";

import SignedInMenu from "./SignedInMenu";

interface Props {
    darkMode: boolean;
    handleThemeChange: () => void;
}


export default function Header({ darkMode, handleThemeChange }: Props) {

    //const { basket } = useAppSelector(state => state.basket);
    const { user } = useAppSelector(state => state.account);
    //const itemCount = basket?.items.reduce((sum, item) => sum + item.quantity, 0)
    const homeLink = {title: 'HOME', path: '/'}
    const midLinks = [
        { title: 'About', path: '/About' },
        { title: 'Catalog', path: '/Catalog' },
        { title: 'Contact', path: '/Contact' }
    ]
    const rightLinks = [
        { title: 'Login', path: '/Login' },
        { title: 'Register', path: '/Register' },
    ]
    const navStyles = {
        textDecoration: 'none',
        color: 'inherit',
        typography: 'h6',
        '&:hover': {
            color: 'grey.500'
        },
        '&.active': {
            color: 'text.secondary'
        }
    }

    return (
        <AppBar position='static' sx={{ mb: 4 }}>
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Box display='flex' alignItems='center'>
                    <Typography variant='h6'
                        component={NavLink}
                        to={homeLink.path}
                        key={homeLink.path}
                        sx={navStyles}
                    >
                        {homeLink.title}
                    </Typography>
                    <Switch checked={darkMode} onChange={handleThemeChange} />
                </Box>

                <List sx={{ display: 'flex' }}>
                    {midLinks.map(({ title, path }) => (
                        <ListItem
                            component={NavLink}
                            to={path}
                            key={path}
                            sx={navStyles}
                        >
                            {title.toUpperCase()}
                        </ListItem>
                    ))}
                </List>
                <Box display='flex' alignItems='center'>
                    {/* <IconButton component={Link} to='/Basket' size='large' sx={{ color: 'inherit' }}>
                        <Badge badgeContent={itemCount} color='secondary'>
                            <ShoppingCart />
                        </Badge>
                    </IconButton> */}
                    {user ? (
                        <SignedInMenu />
                    ) : (
                        <List sx={{ display: 'flex' }}>
                            {rightLinks.map(({ title, path }) => (
                                <ListItem
                                    component={NavLink}
                                    to={path}
                                    key={path}
                                    sx={navStyles}
                                >
                                    {title.toUpperCase()}
                                </ListItem>
                            ))}
                        </List>
                    )
                    }

                </Box>

            </Toolbar>
        </AppBar>
    )
}