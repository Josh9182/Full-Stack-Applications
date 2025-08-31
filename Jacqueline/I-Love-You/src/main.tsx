import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import ILoveYouDarling from './ILoveYouDarling'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ILoveYouDarling />
  </StrictMode>,
)
