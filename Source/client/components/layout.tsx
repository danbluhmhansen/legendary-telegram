import Head from 'next/head';
import Link from 'next/link';
import Image from 'next/future/image';
import { useState } from 'react';

export const siteTitle = 'Legendary Telegram';

export default function Layout({ children }: { children: React.ReactNode; home?: boolean }) {
  const [showBurger, setShowBurger] = useState(false);
  return (
    <div>
      <Head>
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <nav className="navbar" role="navigation" aria-label="main navigation">
        <div className="navbar-brand">
          <Link href="https://bulma.io">
            <a className="navbar-item">
              <Image src="https://bulma.io/images/bulma-logo.png" alt="Logo" width={122} height={28} />
            </a>
          </Link>

          <a
            role="button"
            className={'navbar-burger' + (showBurger ? ' is-active' : '')}
            aria-label="menu"
            aria-expanded={showBurger}
            data-target="navbarBasicExample"
            onClick={() => setShowBurger(!showBurger)}
          >
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
          </a>
        </div>

        <div id="navbarBasicExample" className={'navbar-menu' + (showBurger ? ' is-active' : '')}>
          <div className="navbar-start">
            <Link href="/">
              <a className="navbar-item">Home</a>
            </Link>

            <Link href="/characters">
              <a className="navbar-item">Characters</a>
            </Link>

            <div className="navbar-item has-dropdown is-hoverable">
              <a className="navbar-link">More</a>

              <div className="navbar-dropdown">
                <a className="navbar-item">About</a>
                <a className="navbar-item">Jobs</a>
                <a className="navbar-item">Contact</a>
                <hr className="navbar-divider" />
                <a className="navbar-item">Report an issue</a>
              </div>
            </div>
          </div>

          <div className="navbar-end">
            <div className="navbar-item">
              <div className="buttons">
                <a className="button is-primary">
                  <strong>Sign up</strong>
                </a>
                <a className="button is-light">Log in</a>
              </div>
            </div>
          </div>
        </div>
      </nav>
      <main>{children}</main>
    </div>
  );
}
