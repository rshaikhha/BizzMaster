import { Container, Box, Avatar, Typography, TextField, Button } from "@mui/material";
import { FieldValues, useForm } from "react-hook-form";
import { NavLink, Redirect, useHistory } from "react-router-dom";
import { toast } from "react-toastify";
import agent from "../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { requestCode, updatePassword } from "./accountSlice";


export default function UpdatePassword() {
    const history = useHistory();
    const dispatch = useAppDispatch();
    const { user } = useAppSelector(state => state.account);
    const { register, handleSubmit, setError, formState: { isSubmitting, errors, isValid } } = useForm({
        mode: 'all'
    });


    function handleApiErrors(errors: any) {
        if (errors) {
            console.log(errors)
            errors.forEach((error: string) => {
                
                if (error.includes('Code')) {
                    setError('password', { message: error })
                }  else if (error.includes('Match')) {
                    setError('newPassConfirm', { message: error })
                } else {
                    setError('newPass', { message: error })
                }
            });
        }
    }

    async function submitForm(data: FieldValues) {
        
        agent.Account.updatePassword(data)
                            .then(() =>{
                                toast.success('Registration successful - you can login now');
                                history.push('/login')
                            })
                            .catch(error => handleApiErrors(error))

    }

    if(!user?.token) return <Redirect push to="/login" />
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
                {user?.avatar != '' ? (
                    <Avatar
                        alt="User Avatar"
                        src={user?.avatar}
                        //{user?.avatarUrl}
                        sx={{ width: 150, height: 150 }}
                    />
                ) : (
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}><AccountCircleIcon /></Avatar>
                )}

                <Box
                    component="form"
                    onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>

                    <TextField
                        margin="normal"
                        fullWidth
                        label="Username"
                        value={user?.username}
                        {...register('username')}
                    />
                    <Button
                        disabled={isSubmitting}
                        fullWidth
                        variant="outlined"
                        sx={{ mt: 3, mb: 2 }}
                        onClick = {() => {dispatch(requestCode({username : user?.username}));}}
                    >
                        Request Code
                    </Button>
                    <TextField
                        margin="normal"
                        fullWidth
                        label="Code / Old Password"
                        type="password"
                        {...register('password', { required: 'Code is required' })}
                        error={!!errors.password}
                        helperText={errors?.password?.message}
                    />

                    <TextField
                        margin="normal"
                        fullWidth
                        label="New Password"
                        type="password"
                        {...register('newPass', { required: 'Password is required' })}
                        error={!!errors.newPass}
                        helperText={errors?.newPass?.message}
                    />

                    <TextField
                        margin="normal"
                        fullWidth
                        label="Repeat New Password"
                        type="password"
                        {...register('newPassConfirm', { required: 'Confirm yout Password' })}
                        error={!!errors.newPassConfirm}
                        helperText={errors?.newPassConfirm?.message}
                    />

                    <Button
                        disabled={isSubmitting || !isValid}
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        UPDATE
                    </Button>
                    <Button
                        fullWidth
                        variant="outlined"
                        sx={{ mt: 3, mb: 2 }}
                        component={NavLink} to={'/requestCode'}
                    >
                        Change Password
                    </Button>
                </Box>
            </Box>

        </Container>
    );
}