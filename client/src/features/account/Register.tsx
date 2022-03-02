import { Container, Box, Avatar, Typography, TextField, Button } from "@mui/material";
import { useForm } from "react-hook-form";
import { Redirect, useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import agent from "../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';


export default function Register() {
    const history = useHistory();
    const dispatch = useAppDispatch();
    const {user} = useAppSelector(state => state.account);
    const {register, handleSubmit, setError, formState: {isSubmitting, errors, isValid}} = useForm({
        mode: 'all'
    });


    function handleApiErrors(errors: any) {
        if (errors) {
            errors.forEach((error: string) => {
                if (error.includes('Password')) {
                    setError('password', {message: error})
                } else if (error.includes('Email')) {
                    setError('email', {message: error})
                } else if (error.includes('Username')) {
                    setError('username', {message: error})
                }
            });
        }
    }

    if (!user?.roles.includes('CanRegister')) return (<Redirect to={'/'}></Redirect>)
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
                        REGISTER
                    </Typography>
                    <Box component="form" 
                        onSubmit={handleSubmit((data) => {
                            agent.Account.register(data)
                            .then(() =>{
                                toast.success('Registration successful - you can login now');
                                history.push('/login')
                            })
                            .catch(error => handleApiErrors(error))})} 
                        noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Username"
                            autoFocus
                            {...register('username', {required: 'username is required'})}
                            error={!!errors.username}
                            helperText = {errors?.username?.message}
                        />
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Email address"
                            {...register('email', {
                                required: 'Email is required',
                                pattern: {
                                    value: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,
                                    message: 'Not a valid Email address'
                                }
                            })}
                            error={!!errors.email}
                            helperText = {errors?.email?.message}
                        />
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Password"
                            type="password"
                            {...register('password', {
                                required: 'password is required',
                                pattern: {
                                    value: /(?=^.{6,20}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$/,
                                    message: 'Password Too Simple'
                                } 
                            })}
                            error={!!errors.password}
                            helperText = {errors?.password?.message}
                        />
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