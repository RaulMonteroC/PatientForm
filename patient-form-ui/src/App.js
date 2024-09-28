import { createBrowserRouter, RouterProvider } from "react-router-dom";
import CssBaseline from '@mui/material/CssBaseline';
import RootLayout from "./pages/RootLayout";
import HomePage from "./pages/HomePage";

const router = createBrowserRouter([
    {
        path: '/',
        element: <RootLayout />,
        children: [
            {index: true, element: <HomePage /> }
        ]
    },
]);

export default function App() {
  return (
    <>
      <CssBaseline />
      <RouterProvider router={router} />
    </>
  )
}
