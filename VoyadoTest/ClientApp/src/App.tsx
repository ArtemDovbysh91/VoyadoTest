import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import About from './components/About';
import Query from './components/Query';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Query} />
        <Route path='/About' component={About} />
    </Layout>
);
