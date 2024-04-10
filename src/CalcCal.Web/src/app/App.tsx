import React from 'react';
import {Outlet, ScrollRestoration, useLocation} from "react-router-dom";
import {Bounce, ToastContainer} from "react-toastify";
import {CalculatorPage} from "./calculator/CalculatorPage";
import 'react-toastify/dist/ReactToastify.css'
import {Box} from "@mui/material";

function App() {
  const location = useLocation()

  return(
      <Box sx={{overflowX: 'hidden'}}>
        <ScrollRestoration />
        {
          location.pathname === '/' ?
              <CalculatorPage />
              :
              <Outlet />
        }
        <ToastContainer
            position="bottom-right"
            autoClose={3500}
            limit={1}
            hideProgressBar={false}
            newestOnTop
            closeOnClick
            rtl={false}
            pauseOnFocusLoss={false}
            draggable
            pauseOnHover={false}
            theme="colored"
            transition={Bounce}
        />
      </Box>
  )
}

export default App;

