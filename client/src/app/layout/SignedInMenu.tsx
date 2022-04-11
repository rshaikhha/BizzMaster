import { Button, Menu, Fade, MenuItem, Avatar, Typography } from "@mui/material";
import React from "react";
import { signOut } from "../../features/account/accountSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { NavLink } from "react-router-dom";

export default function SignedInMenu() {
    const dispatch = useAppDispatch();
    const {user} = useAppSelector(state => state.account);

    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);
    const handleClick = (event: any) => {
        setAnchorEl(event.currentTarget);
    };
const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <>
            <Button
                id="fade-button"
                aria-controls={open ? 'fade-menu' : undefined}
                aria-haspopup="true"
                aria-expanded={open ? 'true' : undefined}
                color = 'inherit'
                onClick={handleClick}
                sx = {{typography: 'h6'}}
            >
                {user?.avatar != '' ? (
                            <Avatar
                            alt="User Avatar"
                            src={user?.avatar}
                            sx={{ width: 25, height: 25 }}
                          />
                        ) : (
                            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}><AccountCircleIcon /></Avatar>
                        )}
                
            </Button>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                TransitionComponent={Fade}
            >
                <MenuItem onClick={handleClose} component={NavLink} to={'/dashboard'}>داشبورد</MenuItem>
                <MenuItem onClick={handleClose} component={NavLink} to={'/profile'}>پروفایل</MenuItem>
                {user?.roles.includes('CanInvite') ?  
                    <MenuItem onClick={handleClose} component={NavLink} to={'/invite'}>دعوت همکار</MenuItem>
                    : ''
                }
                <MenuItem onClick={() => dispatch(signOut())}>Logout</MenuItem>
            </Menu>
        </>
    );
}