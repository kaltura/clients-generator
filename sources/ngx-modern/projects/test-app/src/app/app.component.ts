import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { KalturaClient } from 'kaltura-ngx-client';

// Note: After running exec.php ngxModern, the following imports will be available:
// import { MediaListAction, UserListAction, CategoryListAction } from 'kaltura-ngx-client';
// import { KalturaMediaEntry, KalturaUser, KalturaCategory, KalturaMediaListResponse, KalturaUserListResponse, KalturaCategoryListResponse } from 'kaltura-ngx-client';

interface ApiResult {
  entries?: any[];
  users?: any[];
  categories?: any[];
  error?: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="container">
      <h1>Kaltura ngxModern Client Test Page</h1>
      
      <div class="config-section">
        <h2>Configuration</h2>
        <div class="form-group">
          <label for="serverUrl">Server API URL:</label>
          <input 
            id="serverUrl" 
            type="text" 
            [(ngModel)]="serverUrl" 
            placeholder="https://www.kaltura.com/api_v3"
            class="input-field"
          />
        </div>
        
        <div class="form-group">
          <label for="ks">Kaltura Session (KS):</label>
          <input 
            id="ks" 
            type="text" 
            [(ngModel)]="ks" 
            placeholder="Enter your KS token"
            class="input-field"
          />
        </div>
        
        <div class="form-group">
          <label for="partnerId">Partner ID:</label>
          <input 
            id="partnerId" 
            type="number" 
            [(ngModel)]="partnerId" 
            placeholder="Enter your Partner ID"
            class="input-field"
          />
        </div>
        
        <button 
          (click)="fetchData()" 
          [disabled]="loading || !serverUrl || !ks"
          class="fetch-button"
        >
          {{ loading ? 'Loading...' : 'Fetch Data (List Entries, Users, Categories)' }}
        </button>
      </div>
      
      <div class="results-section" *ngIf="result">
        <div *ngIf="result.error" class="error-message">
          <h3>Error</h3>
          <pre>{{ result.error }}</pre>
        </div>
        
        <div class="result-panel" *ngIf="result.entries">
          <h3>Media Entries ({{ result.entries.length }} found)</h3>
          <div class="result-list">
            <div *ngFor="let entry of result.entries" class="result-item">
              <strong>{{ entry.name || entry.id }}</strong>
              <span class="item-id">ID: {{ entry.id }}</span>
              <span class="item-type" *ngIf="entry.mediaType">Type: {{ entry.mediaType }}</span>
              <span class="item-status" *ngIf="entry.status">Status: {{ entry.status }}</span>
            </div>
            <div *ngIf="result.entries.length === 0" class="empty-message">
              No entries found
            </div>
          </div>
        </div>
        
        <div class="result-panel" *ngIf="result.users">
          <h3>Users ({{ result.users.length }} found)</h3>
          <div class="result-list">
            <div *ngFor="let user of result.users" class="result-item">
              <strong>{{ user.screenName || user.email || user.id }}</strong>
              <span class="item-id">ID: {{ user.id }}</span>
              <span class="item-email" *ngIf="user.email">Email: {{ user.email }}</span>
            </div>
            <div *ngIf="result.users.length === 0" class="empty-message">
              No users found
            </div>
          </div>
        </div>
        
        <div class="result-panel" *ngIf="result.categories">
          <h3>Categories ({{ result.categories.length }} found)</h3>
          <div class="result-list">
            <div *ngFor="let category of result.categories" class="result-item">
              <strong>{{ category.name || category.id }}</strong>
              <span class="item-id">ID: {{ category.id }}</span>
              <span class="item-path" *ngIf="category.fullName">Path: {{ category.fullName }}</span>
            </div>
            <div *ngIf="result.categories.length === 0" class="empty-message">
              No categories found
            </div>
          </div>
        </div>
      </div>
      
      <div class="instructions">
        <h2>Instructions</h2>
        <ol>
          <li>Enter your Kaltura Server API URL (e.g., https://www.kaltura.com/api_v3)</li>
          <li>Enter your Kaltura Session (KS) token</li>
          <li>Enter your Partner ID</li>
          <li>Click "Fetch Data" to list entries, users, and categories</li>
        </ol>
        <p><strong>Note:</strong> This test page requires the ngxModern client to be generated first using:</p>
        <pre>php exec.php ngxModern [output_path]</pre>
      </div>
    </div>
  `,
  styles: [`
    .container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 20px;
      font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, sans-serif;
    }
    
    h1 {
      color: #00b4d8;
      border-bottom: 2px solid #00b4d8;
      padding-bottom: 10px;
    }
    
    .config-section {
      background: #f8f9fa;
      padding: 20px;
      border-radius: 8px;
      margin-bottom: 20px;
    }
    
    .form-group {
      margin-bottom: 15px;
    }
    
    .form-group label {
      display: block;
      margin-bottom: 5px;
      font-weight: 600;
      color: #333;
    }
    
    .input-field {
      width: 100%;
      padding: 10px;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-size: 14px;
      box-sizing: border-box;
    }
    
    .input-field:focus {
      outline: none;
      border-color: #00b4d8;
      box-shadow: 0 0 0 2px rgba(0, 180, 216, 0.2);
    }
    
    .fetch-button {
      background: #00b4d8;
      color: white;
      border: none;
      padding: 12px 24px;
      border-radius: 4px;
      font-size: 16px;
      cursor: pointer;
      transition: background 0.2s;
    }
    
    .fetch-button:hover:not(:disabled) {
      background: #0096c7;
    }
    
    .fetch-button:disabled {
      background: #ccc;
      cursor: not-allowed;
    }
    
    .results-section {
      margin-top: 20px;
    }
    
    .result-panel {
      background: white;
      border: 1px solid #ddd;
      border-radius: 8px;
      padding: 15px;
      margin-bottom: 15px;
    }
    
    .result-panel h3 {
      margin-top: 0;
      color: #333;
      border-bottom: 1px solid #eee;
      padding-bottom: 10px;
    }
    
    .result-list {
      max-height: 300px;
      overflow-y: auto;
    }
    
    .result-item {
      padding: 10px;
      border-bottom: 1px solid #f0f0f0;
      display: flex;
      flex-direction: column;
      gap: 4px;
    }
    
    .result-item:last-child {
      border-bottom: none;
    }
    
    .result-item strong {
      color: #333;
    }
    
    .item-id, .item-type, .item-status, .item-email, .item-path {
      font-size: 12px;
      color: #666;
    }
    
    .empty-message {
      color: #999;
      font-style: italic;
      padding: 10px;
    }
    
    .error-message {
      background: #ffe6e6;
      border: 1px solid #ff6b6b;
      border-radius: 8px;
      padding: 15px;
      margin-bottom: 15px;
    }
    
    .error-message h3 {
      color: #ff6b6b;
      margin-top: 0;
    }
    
    .error-message pre {
      background: #fff;
      padding: 10px;
      border-radius: 4px;
      overflow-x: auto;
      font-size: 12px;
    }
    
    .instructions {
      background: #e8f4f8;
      padding: 20px;
      border-radius: 8px;
      margin-top: 30px;
    }
    
    .instructions h2 {
      margin-top: 0;
      color: #0077b6;
    }
    
    .instructions ol {
      padding-left: 20px;
    }
    
    .instructions li {
      margin-bottom: 8px;
    }
    
    .instructions pre {
      background: #333;
      color: #0f0;
      padding: 10px;
      border-radius: 4px;
      overflow-x: auto;
    }
  `]
})
export class AppComponent {
  serverUrl = '';
  ks = '';
  partnerId: number | null = null;
  loading = false;
  result: ApiResult | null = null;

  constructor(private kalturaClient: KalturaClient) {}

  async fetchData(): Promise<void> {
    if (!this.serverUrl || !this.ks) {
      this.result = { error: 'Please enter both Server URL and KS' };
      return;
    }

    this.loading = true;
    this.result = null;

    try {
      // Configure the Kaltura client with provided options
      this.kalturaClient.setOptions({
        endpointUrl: this.serverUrl,
        clientTag: 'ngx-modern-test-app'
      });

      this.kalturaClient.setDefaultRequestOptions({
        ks: this.ks,
        ...(this.partnerId ? { partnerId: this.partnerId } : {})
      });

      // Note: The actual API calls require generated types from exec.php
      // This is a placeholder that demonstrates the pattern.
      // After generation, use:
      //
      // const entriesResult = await firstValueFrom(
      //   this.kalturaClient.request(new MediaListAction({}))
      // );
      //
      // const usersResult = await firstValueFrom(
      //   this.kalturaClient.request(new UserListAction({}))
      // );
      //
      // const categoriesResult = await firstValueFrom(
      //   this.kalturaClient.request(new CategoryListAction({}))
      // );

      this.result = {
        entries: [],
        users: [],
        categories: [],
        error: 'API types not yet generated. Run "php exec.php ngxModern" first to generate the client types, then uncomment the API calls in this component.'
      };

    } catch (error: any) {
      this.result = {
        error: error?.message || 'An error occurred while fetching data'
      };
    } finally {
      this.loading = false;
    }
  }
}
