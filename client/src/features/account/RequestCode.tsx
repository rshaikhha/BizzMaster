import { Container, Box, Avatar, Typography, TextField, Button } from "@mui/material";
import { useForm, FieldValues } from "react-hook-form";
import { useHistory } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { requestCode } from "./accountSlice";
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';


export default function RequestCode() {
    const history = useHistory();
    const dispatch = useAppDispatch();
    
    const {user} = useAppSelector(state => state.account);


    const {register, handleSubmit, formState: {isSubmitting, errors, isValid}} = useForm({
        mode: 'all'
    });



    async function submitForm(data: FieldValues) {
        
        const result = await dispatch(requestCode(data));
        if (result.type.endsWith('fulfilled')) {
            
            console.log('done');
            if(user) {
                history.push('/login');
            }
            else {
                history.push('/updatePassword')
            }
            
        } else {
            console.log('failed')
        }

    }

    
    return (
            <Container component="main" maxWidth="xs">
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Request Code 
                    </Typography>
                    <Box 
                    component="form" 
                    onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Phone Number"
                            autoFocus
                            {...register('username', {
                                required: 'please enter your phone number',
                                pattern: {
                                    value: /^09\d{9}$/,
                                    message: 'Not Valid'
                                }
                            })}
                            error={!!errors.username}
                            helperText = {errors?.username?.message}
                        />
                        {/* <TextField
                            margin="normal"
                            fullWidth
                            label="Password"
                            type="password"
                            {...register('password', {required: 'password is required'})}
                            error={!!errors.password}
                            helperText = {errors?.password?.message}
                        /> */}
                        <Button
                            disabled = {isSubmitting || !isValid}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            SUBMIT
                        </Button>
                    </Box>
                </Box>

            </Container>
    );
}