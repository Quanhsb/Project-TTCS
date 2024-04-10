import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import { useDispatch, useSelector } from "react-redux";
import { login } from "../Redux/authSlice";
import { useHistory } from "react-router-dom";
import logo from '../assets/logo.jpg';
export default function SignIn() {
  const history = useHistory();
  const dispatch = useDispatch();
  const { loginType } = useSelector((state) => state.auth)
  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    dispatch(login({ userName: data.get("username"), password: data.get("password") })).then(() => {
      history.push("/");
    });
  };
  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <img src={logo} alt="logo" style={{ width: 200, height: 200 }} />
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="Tên đăng nhập"
            name="username"
            autoComplete="username"
            inputProps={{ minLength: 5 }}
            sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, "& .MuiInputLabel-asterisk": { display: "none" } }}
            autoFocus
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Mật khẩu"
            type="password"
            id="password"
            inputProps={{ minLength: 5, maxLength: 20 }}
            sx={{ "& .MuiInputBase-root": { height: 60 }, input: { border: "none" }, "& .MuiInputLabel-asterisk": { display: "none" } }}
            autoComplete="current-password"
          />
          <div style={{ display: 'flex', justifyContent: 'center', color: 'red' }}>{loginType === false ? 'Tên đăng nhập hoặc mật khẩu không đúng' : ''}</div>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Đăng nhập
          </Button>
        </Box>
      </Box>
    </Container>
  );
}