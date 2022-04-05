import { Container, Box, Avatar, Typography, TextField, Button } from "@mui/material";
import { useForm, FieldValues } from "react-hook-form";
import { NavLink, Redirect, useHistory } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { updateProfile } from "./accountSlice";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import { useState } from "react";



export default function Profile() {
    const history = useHistory();
    const dispatch = useAppDispatch();
    
    const {user} = useAppSelector(state => state.account);

    

    const {register, handleSubmit, formState: {isSubmitting, errors, isValid}} = useForm({
        mode: 'all'
    });

    const [image, setImage] = useState({ preview: user?.avatar || "", raw: ""});
    const handleChange = (e: any) => {
        if (e.target.files.length) {
            setImage({
                preview: URL.createObjectURL(e.target.files[0]),
                raw: e.target.files[0],
            });
        }
      };


    function getBase64(file: any)  {
        console.log(file);
        return new Promise((resolve, reject) => {
            const fileReader = new FileReader();
            fileReader.readAsDataURL(file)
            fileReader.onload = () => {
              resolve(fileReader.result);
            }
            fileReader.onerror = (error) => {
              reject(error);
            }
          })
    }


    async function submitForm(data: FieldValues) {
        if (image.raw != "") {
            data.avatar = await getBase64(image.raw)
        } else {
            data.avatar = user?.avatar
        }
        console.log(data)
        const result = await dispatch(updateProfile(data));
        if (result.type.endsWith('fulfilled')) {
            
            console.log('done');
            window.location.reload();
        } else {
            console.log('failed')
        }

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
                            src={image.preview}
                            //{user?.avatarUrl}
                            sx={{ width: 150, height: 150 }}
                          />
                        ) : (
                            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}><AccountCircleIcon /></Avatar>
                        )}


                        <label htmlFor="upload-button"><h5>Change Photo</h5></label>
                        <TextField type="file" id="upload-button" style={{ display: 'none' }} onChange={handleChange} />
                        

                    <Box 
                    component="form" 
                    onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
                        
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Username"
                            value= {user?.username}
                            {...register('username')}
                        />
                        <TextField
                            margin="normal"
                            fullWidth
                            label="First Name"
                            defaultValue= {user?.firstName}
                            InputLabelProps={{ shrink: true }}
                            {...register('firstName')}
                        />
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Last Name"
                            defaultValue= {user?.lastName}
                            InputLabelProps={{ shrink: true }}
                            {...register('lastName')}
                        />
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Email"
                            defaultValue= {user?.email}
                            InputLabelProps={{ shrink: true }}
                            {...register('email' , {
                                pattern: {
                                    value: /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/,
                                    message: 'Not Valid'
                                }
                            })}
                            error={!!errors.email}
                            helperText = {errors?.email?.message}
                        />
                        
                        <Button
                            disabled = {isSubmitting || !isValid}
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
                            component={NavLink} to={'/UpdatePassword'}
                        >
                            Change Password
                        </Button>
                    </Box>
                </Box>

            </Container>
    );
}