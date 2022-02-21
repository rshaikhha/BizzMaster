import { Button, Menu, Fade, MenuItem } from "@mui/material";
import React from "react";
// import { signOut } from "../../feature/account/accountSlice";
// import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function SignedInMenu() {
    // const dispatch = useAppDispatch();
    // const {user} = useAppSelector(state => state.account);

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
                {/* {user?.email} */} Email@email.com
            </Button>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                TransitionComponent={Fade}
            >
                <MenuItem onClick={handleClose}>Profile</MenuItem>
                <MenuItem onClick={handleClose}>My account</MenuItem>
                {/* <MenuItem onClick={() => dispatch(signOut())}>Logout</MenuItem> */}
            </Menu>
        </>
    );
}